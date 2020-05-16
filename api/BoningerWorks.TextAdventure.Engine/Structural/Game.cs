using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Enums;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Structural
{
	public class Game
	{
		public static Game Deserialize(string json) => new Game(GameMap.Deserialize(json));

		public Player Player { get; }
		public Areas Areas { get; }
		public Items Items { get; }
		public Entities Entities { get; }
		public Commands Commands { get; }
		public Reactions Reactions { get; }
		public DevelopmentMap Development { get; }

		private readonly Action<ResultBuilder> _actionStart;
		private readonly Action<ResultBuilder> _actionEnd;
		private readonly Action<ResultBuilder> _actionPrompt;
		private readonly Action<ResultBuilder> _actionFail;
		private readonly ImmutableArray<ActionMap> _actionMapsAreaAmbiguous;
		private readonly ImmutableArray<ActionMap> _actionMapsItemAmbiguous;
		private readonly OptionsMap _optionsMap;

		private Game(GameMap gameMap)
		{
			// Set player
			Player = new Player(gameMap.PlayerMap);
			// Set areas
			Areas = new Areas(gameMap.AreaMaps);
			// Set items
			Items = new Items(gameMap.ItemMaps);
			// Set entities
			Entities = new Entities(Player, Areas, Items);
			// Set commands
			Commands = new Commands(Entities, gameMap.CommandMaps);
			// Set reactions
			Reactions = new Reactions(Entities, Commands, gameMap.ReactionMaps, gameMap.ConditionAreaMap, gameMap.ConditionItemMap);
			// Set development
			Development = gameMap.DevelopmentMap;
			// Run through commands
			foreach (var command in Commands)
			{
				// Check command fail action
				command.CheckFail(Entities, Reactions);
			}
			// Set start action
			_actionStart = Reactions.CreateAction(gameMap.ActionMapsStart);
			// Set end action
			_actionEnd = Reactions.CreateAction(gameMap.ActionMapsEnd);
			// Set fail action
			_actionFail = Reactions.CreateAction(gameMap.ActionMapsFail);
			// Set prompt action
			_actionPrompt = Reactions.CreateAction(gameMap.ActionMapsPrompt);
			// Set ambiguous area action maps
			_actionMapsAreaAmbiguous = gameMap.ActionMapsAreaAmbiguous;
			// Test ambiguous area action maps
			_ = Reactions.CreateAction(_actionMapsAreaAmbiguous);
			// Set ambiguous item action maps
			_actionMapsItemAmbiguous = gameMap.ActionMapsItemAmbiguous;
			// Test ambiguous item action maps
			_ = Reactions.CreateAction(_actionMapsItemAmbiguous);
			// Set options map
			_optionsMap = gameMap.OptionsMap;
		}

		public Result New()
		{
			// Create entities
			var entities = ImmutableDictionary.CreateBuilder<Id, Entity>();
			// Run through entities
			for (int i = 0; i < Entities.Count; i++)
			{
				// Add entity
				entities.Add(Entities[i].Id, new Entity());
			}
			// Create state
			var state = new State(entities.ToImmutable(), complete: false);
			// Create result
			var result = new ResultBuilder(this, state);
			// Execute start
			_actionStart(result);
			// Return result
			return _Finalize(result);
		}

		public Result Execute(State state, string? input)
		{
			// Check if state is complete
			if (state.Complete)
			{
				// Return result
				return new Result(state, ImmutableList<Message>.Empty, ImmutableList<Message>.Empty);
			}
			// Create result
			var result = new ResultBuilder(this, state);
			// Execute input
			_Execute(result, input);
			// Return result
			return _Finalize(result);
		}
		
		private void _Execute(ResultBuilder result, string? input)
		{
			// Run through ignored characters
			for (int i = 0; i < _optionsMap.IgnoredCharacters.Length; i++)
			{
				// Set input
				input = input?.Replace(_optionsMap.IgnoredCharacters[i].ToString(), string.Empty);
			}
			// Try to get match
			var match = Commands.TryGetMatch(input);
			// Check if match does not exist
			if (match == null)
			{
				// Execute fail
				_actionFail(result);
				// Return
				return;
			}
			// Get match parts
			var matchParts = match.Parts;
			// Create path entities
			var entitiesPath = ImmutableList.CreateBuilder<IEntity>();
			// Run through match parts
			for (int i = 0; i < matchParts.Count; i++)
			{
				var matchPart = matchParts[i];
				// Get match entities
				var entitiesMatch = matchPart.Entities;
				// Check if more than one match entity
				if (entitiesMatch.Count > 1)
				{
					// Get entity type
					var typeEntity = entitiesMatch[0].GetType();
					// Create ambiguous entities
					var entitiesAmbiguous = entitiesMatch.ToImmutableList();
					// Get ambiguous action maps
					var actionMapsAmbiguous = (typeEntity.Equals(typeof(Area)) ? _actionMapsAreaAmbiguous : (ImmutableArray<ActionMap>?)null)
						?? (typeEntity.Equals(typeof(Item)) ? _actionMapsItemAmbiguous : (ImmutableArray<ActionMap>?)null)
						?? throw new InvalidOperationException($"Ambiguous action for entity type ({typeEntity.Name}) could not be handled.");
					// Create ambiguous action
					var actionAmbiguous = Reactions.CreateAction(actionMapsAmbiguous, entitiesAmbiguous: entitiesAmbiguous);
					// Execute ambiguous action
					actionAmbiguous(result);
					// Return
					return;
				}
				// Add path entity
				entitiesPath.Add(entitiesMatch[0]);
			}
			// Create reaction query
			var reactionQuery = new ReactionQuery(match.Command, entitiesPath.ToImmutable());
			// Get reaction result
			var reactionResult = Reactions.GetResult(result.State, reactionQuery);
			// Check reaction outcome
			switch (reactionResult.Outcome)
			{
				case EReactionOutcome.Nothing:
					// Check if command has fail
					if (reactionQuery.Command.HasFail())
					{
						// Execute command fail
						reactionQuery.Command.ExecuteFail(result, reactionQuery.Parts);
					}
					else
					{
						// Execute fail
						_actionFail(result);
					}
					break;
				case EReactionOutcome.OutOfContext:
					// Execute fail
					_actionFail(result);
					break;
				case EReactionOutcome.Success:
					// Get reactions
					var reactions = reactionResult.Reactions;
					// Run through reactions
					for (int i = 0; i < reactions.Length; i++)
					{
						var reaction = reactions[i];
						// Execute reaction
						reaction.Execute(result);
					}
					break;
				default:
					// Throw error
					throw new InvalidOperationException($"Reaction result outcome ({reactionResult.Outcome}) could not be handled.");
			}
		}

		private Result _Finalize(ResultBuilder result)
		{
			// Check if complete
			if (result.State.Complete)
			{
				// Execute end
				_actionEnd(result);
				// Create end result
				var resultEnd = new Result(result.State, result.Messages.ToImmutable(), ImmutableList<Message>.Empty);
				// Return end result
				return resultEnd;
			}
			// Create prompt result
			var resultPrompt = new ResultBuilder(this, result.State);
			// Execute prompt
			_actionPrompt(resultPrompt);
			// Check if complete
			if (resultPrompt.State.Complete)
			{
				// Set state
				result.State = resultPrompt.State;
				// Add messages
				result.Messages.AddRange(resultPrompt.Messages);
				// Execute end
				_actionEnd(result);
				// Create end result
				var resultEnd = new Result(result.State, result.Messages.ToImmutable(), ImmutableList<Message>.Empty);
				// Return end result
				return resultEnd;
			}
			// Create final result
			var resultFinal = new Result(resultPrompt.State, result.Messages.ToImmutable(), resultPrompt.Messages.ToImmutable());
			// Return final result
			return resultFinal;
		}
	}
}

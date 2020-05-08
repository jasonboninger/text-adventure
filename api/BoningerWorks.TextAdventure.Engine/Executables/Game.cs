﻿using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Enums;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Text;

namespace BoningerWorks.TextAdventure.Engine.Executables
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

		private readonly Action<ResultBuilder> _actionStart;
		private readonly Action<ResultBuilder> _actionEnd;
		private readonly Action<ResultBuilder> _actionFail;

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
		}

		public Result New()
		{
			// Create entities
			var entities = ImmutableDictionary.CreateBuilder<Symbol, Entity>();
			// Run through entities
			for (int i = 0; i < Entities.Count; i++)
			{
				// Add entity
				entities.Add(Entities[i].Symbol, new Entity());
			}
			// Create state
			var state = new State(entities.ToImmutable());
			// Create result
			var result = new ResultBuilder(this, state);
			// Execute start
			_actionStart(result);
			// Return result
			return result.ToImmutable();
		}

		public Result Execute(State state, string? input)
		{
			// Try to get match
			var match = Commands.TryGetMatch(input);
			// Check if match does not exist
			if (match == null)
			{
				// Create fail result
				var resultFail = new ResultBuilder(this, state);
				// Execute fail
				_actionFail(resultFail);
				// Return result
				return resultFail.ToImmutable();
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
					// Create text
					var text = new StringBuilder("Input matched more than one entity. Did you mean ");
					// Run through match entities
					for (int k = 0; k < entitiesMatch.Count; k++)
					{
						var entityMatch = entitiesMatch[k];
						// Check if not first
						if (k > 0)
						{
							// Add comma
							text.Append(", ");
						}
						// Check if last
						if (k == entitiesMatch.Count - 1)
						{
							// Add or
							text.Append("or ");
						}
						// Add name
						text.Append(entityMatch.Names.Name);
					}
					// Add question mark
					text.Append("?");
					// Create message
					var message = new Message(text.ToString());
					// Return result
					return new Result(state, ImmutableList.Create(message));
				}
				// Add path entity
				entitiesPath.Add(entitiesMatch[0]);
			}
			// Create reaction query
			var reactionQuery = new ReactionQuery(match.Command, entitiesPath.ToImmutable());
			// Get reaction result
			var reactionResult = Reactions.GetResult(state, reactionQuery);
			// Create result
			var result = new ResultBuilder(this, state);
			// Check reaction outcome
			switch (reactionResult.Outcome)
			{
				case EReactionOutcome.Nothing:
					// Execute command fail
					reactionQuery.Command.ExecuteFail(result, reactionQuery.Parts);
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
					throw new InvalidOperationException($"Reaction result out come ({reactionResult.Outcome}) could not be handled.");
			}
			// Return result
			return result.ToImmutable();
		}
	}
}

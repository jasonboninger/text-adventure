using BoningerWorks.TextAdventure.Core.Utilities;
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

		private Game(GameMap gameMap)
		{
			// Check if game map does not exist
			if (gameMap == null)
			{
				// Throw error
				throw new ArgumentException("Game map cannot be null.", nameof(gameMap));
			}
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
			Reactions = new Reactions(Entities, Commands, gameMap.ReactionMaps);
		}

		public State New()
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
			// Return state
			return state;
		}

		public Result Execute(State state, string? input)
		{
			// Try to get match
			var match = Commands.TryGetMatch(input);
			// Check if match does not exist
			if (match == null)
			{
				// Return result
				return new Result(state, ImmutableList<Message>.Empty);
			}
			// Get match parts
			var matchParts = match.Parts;
			// Create path parts
			var pathParts = ImmutableList.CreateBuilder<ReactionPathPart>();
			// Run through match parts
			for (int i = 0; i < matchParts.Count; i++)
			{
				var matchPart = matchParts[i];
				// Get entities
				var entities = matchPart.Entities;
				// Check if more than one entity
				if (entities.Count > 1)
				{
					// Create text
					var text = new StringBuilder("Input matched more than one entity. Did you mean ");
					// Run through entities
					for (int k = 0; k < entities.Count; k++)
					{
						var entity = entities[k];
						// Check if not first
						if (k > 0)
						{
							// Add comma
							text.Append(", ");
						}
						// Check if last
						if (k == entities.Count - 1)
						{
							// Add or
							text.Append("or ");
						}
						// Add name
						text.Append(entity.Names.Name);
					}
					// Add question mark
					text.Append("?");
					// Create message
					var message = new Message(text.ToString());
					// Return result
					return new Result(state, ImmutableList.Create(message));
				}
				// Create path part
				var pathPart = new ReactionPathPart(matchPart.Input, entities[0]);
				// Add path part
				pathParts.Add(pathPart);
			}
			// Get command
			var command = match.Command;
			// Create path
			var path = new ReactionPath(command, pathParts.ToImmutable());
			// Try to get reactions
			var reactions = Reactions.TryGet(path);
			// Check if reactions exists
			if (!reactions.HasValue)
			{
				// Return result
				return new Result(state, ImmutableList<Message>.Empty);
			}
			// Create messages
			var messages = ImmutableList.CreateBuilder<Message>();
			// Run through reactions
			for (int i = 0; i < reactions.Value.Length; i++)
			{
				var reaction = reactions.Value[i];
				// Execute reaction
				var result = reaction.Execute(state);
				// Set state
				state = result.State;
				// Add messages
				messages.AddRange(result.Messages);
			}
			// Return result
			return new Result(state, messages.ToImmutable());
		}
	}
}

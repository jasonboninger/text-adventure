using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Errors;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Immutable;

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
				throw new ValidationError("Game map cannot be null.");
			}
			// Set areas
			Areas = new Areas(gameMap.AreaMaps);
			// Set player
			Player = new Player(Areas, gameMap.PlayerMap);
			// Set items
			Items = new Items(Player, Areas, gameMap.ItemMaps);
			// Set entities
			Entities = new Entities(Player, Areas, Items);
			// Set commands
			Commands = new Commands(Items, gameMap.CommandMaps);
			// Set reactions
			Reactions = new Reactions(Entities, Items, Commands, gameMap.ReactionMaps);
		}

		public State New()
		{
			// Create entities
			var entities = ImmutableDictionary.CreateBuilder<Symbol, Entity>();
			// Run through entities
			for (int i = 0; i < Entities.Count; i++)
			{
				var entity = Entities[i];
				// Add entity
				entities.Add(entity.Symbol, entity.Create());
			}
			// Create state
			var state = new State(entities.ToImmutable());
			// Return state
			return state;
		}

		public Result Execute(State state, string? input)
		{
			// Create messages
			var messages = ImmutableList.CreateBuilder<Message>();
			// Try to get command match
			try
			{
				// Try to get command match
				var commandMatch = Commands.TryGetMatch(input);
				// Check if command match exists
				if (commandMatch != null)
				{
					// Get command
					var command = commandMatch.Command;
					// Try to get reaction
					var reaction = Reactions.TryGet(command);
					// Check if reaction exists
					if (reaction != null)
					{
						// Get command items
						var commandItems = command.CommandItems;
						// Run through command items
						for (int i = 0; i < commandItems.Length; i++)
						{
							var commandItem = commandItems[i];
							// Get item
							var item = commandMatch.CommandItemToItemMappings[commandItem];
							// Try to get next reaction
							if (reaction.Next == null || !reaction.Next.TryGetValue(item.Symbol, out var reactionNext))
							{
								// Set no reaction
								reaction = null;
								// Stop loop
								break;
							}
							// Set reaction
							reaction = reactionNext;
						}
						// Check if reaction exists and matches exist
						if (reaction != null && reaction.Matches.HasValue)
						{
							// Get matches
							var matches = reaction.Matches.Value;
							// Run through matches
							for (int i = 0; i < matches.Length; i++)
							{
								var match = matches[i];
								// Execute match
								var result = match.Execute(state);
								// Set state
								state = result.State;
								// Add messages
								messages.AddRange(result.Messages);
							}
						}
					}
				}
			}
			catch (GenericException<AmbiguousCommandItemMatchError> exception)
			{



				// Discard exception
				_ = exception;
				// Throw error
				throw;



			}
			// Return result
			return new Result(state, messages.ToImmutable());
		}
	}
}

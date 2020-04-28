using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Errors;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Game
	{
		public static Symbol ValueTrue { get; } = new Symbol("TRUE");
		public static Symbol ValueFalse { get; } = new Symbol("FALSE");

		public static Game Deserialize(string json) => new Game(GameMap.Deserialize(json));

		public Player Player { get; }
		public Areas Areas { get; }
		public Items Items { get; }
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
			// Set player
			Player = new Player(gameMap.PlayerMap);
			// Set areas
			Areas = new Areas(gameMap.AreaMaps);
			// Set items
			Items = new Items(Player, Areas, gameMap.ItemMaps);
			// Set commands
			Commands = new Commands(Items, gameMap.CommandMaps);
			// Set reactions
			Reactions = new Reactions(Player, Areas, Items, Commands, gameMap.ReactionMaps);
		}

		public State New()
		{
			// Create entity states
			var entityStates = ImmutableDictionary.CreateBuilder<Symbol, Entity>();
			// Add player state
			entityStates.Add(Player.Symbol, new Entity(data: null, customData: null));
			// Run through areas
			foreach (var area in Areas)
			{
				// Add area state
				entityStates.Add(area, new Entity(data: null, customData: null));
			}
			// Run through items
			foreach (var item in Items)
			{
				// Add item state
				entityStates.Add
					(
						item.Symbol, 
						new Entity
							(
								data: ImmutableDictionary.CreateRange(new KeyValuePair<Symbol, Symbol>[] 
								{
									KeyValuePair.Create(Item.DatumActive, item.Active != false ? ValueTrue : ValueFalse),
									KeyValuePair.Create(Item.DatumLocation, item.Location)
								}),
								customData: null
							)
					);
			}
			// Create game state
			var gameState = new State(entityStates.ToImmutable());
			// Return game state
			return gameState;
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
						// Check if reaction exists and actions exist
						if (reaction != null && reaction.Actions.HasValue)
						{
							// Get actions
							var actions = reaction.Actions.Value;
							// Run through actions
							for (int i = 0; i < actions.Length; i++)
							{
								var action = actions[i];
								// Execute action
								var result = action.Execute(state);
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

using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using BoningerWorks.TextAdventure.Engine.Exceptions;
using BoningerWorks.TextAdventure.Engine.Exceptions.Data;
using BoningerWorks.TextAdventure.Engine.Maps;
using BoningerWorks.TextAdventure.Engine.States;
using BoningerWorks.TextAdventure.Engine.States.Data;
using BoningerWorks.TextAdventure.Engine.States.Messages;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Game
	{
		public Items Items { get; }
		public Commands Commands { get; }

		public Game(GameBlueprint gameBlueprint)
		{
			// Check if game blueprint does not exist
			if (gameBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Game blueprint cannot be null.", nameof(gameBlueprint));
			}
			// Set items
			Items = _CreateItems(gameBlueprint.Player);
			// Set commands
			Commands = _CreateCommands(Items, gameBlueprint.Commands);
		}

		public GameState New()
		{
			// Create game state
			var gameState = GameState.Create();
			// Add global state
			gameState.EntityStates.Add(Symbol.Global, EntityState.CreateGlobal());
			// Add player state
			gameState.EntityStates.Add(Symbol.Player, EntityState.CreatePlayer());
			// Run through items
			for (int i = 0; i < Items.Count; i++)
			{
				var item = Items[i];
				// Create item data
				var itemData = ItemData.Create(item.Location, item.Active);
				// Create item state
				var itemState = EntityState.CreateItem(itemData);
				// Add item state
				gameState.EntityStates.Add(item.Symbol, itemState);
			}
			// Return game state
			return gameState;
		}

		public List<MessageState> Execute(GameState state, string input)
		{
			// Create messages
			var messages = new List<MessageState>();
			// Try to create command match
			try
			{
				// Try to create command match
				if (Commands.TryCreateMatch(input, out var commandMatch))
				{
					// Get command
					var command = commandMatch.Command;
					// Get command handler
					var commandHandler = Commands.GetHandler(command);
					// Run through command item symbols
					for (int i = 0; i < command.ItemSymbols.Length; i++)
					{
						var commandItemSymbol = command.ItemSymbols[i];
						// Get item
						var item = commandMatch.ItemSymbolToItemMappings[command.ItemSymbols[i]];
						// Try to get next command handler
						if (!commandHandler.Next.TryGetValue(item.Symbol, out var commandHandlerNext))
						{
							// Set no command handler
							commandHandler = null;
							// Stop loop
							break;
						}
						// Set command handler
						commandHandler = commandHandlerNext;
					}
					// Check if command handler exists
					if (commandHandler != null)
					{
						// Run through actions
						for (int i = 0; i < commandHandler.Actions.Length; i++)
						{
							var action = commandHandler.Actions[i];
							// Execute action
							messages.AddRange(action.Execute(state));
						}
					}
				}
			}
			catch (GenericException<AmbiguousCommandItemMatchData> exception)
			{



				// Discard exception
				_ = exception;
				// Throw error
				throw;



			}
			// Return messages
			return messages;
		}

		private static Items _CreateItems(PlayerBlueprint playerBlueprint)
		{
			// Check if player blueprint does not exist
			if (playerBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Player blueprint cannot be null.", nameof(playerBlueprint));
			}
			// Create items
			var items = Enumerable.Empty<Item>()
				.Concat(playerBlueprint.Items?.Select(kv => new Item(new Symbol(kv.Key), Symbol.Player, kv.Value)) ?? Enumerable.Empty<Item>());
			// Return items
			return new Items(items);
		}

		private static Commands _CreateCommands(Items items, Dictionary<string, CommandBlueprint> commandBlueprints)
		{
			// Check if command blueprints does not exist
			if (commandBlueprints == null)
			{
				// Throw error
				throw new ArgumentException("Command blueprints cannot be null.", nameof(commandBlueprints));
			}
			// Create command maps
			var commandMaps = Enumerable.Empty<CommandMap>()
				.Concat(items.SelectMany(i => i.CommandMaps))
				.ToImmutableList();
			// Return commands
			return new Commands(items, commandBlueprints, commandMaps);
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using BoningerWorks.TextAdventure.Engine.Exceptions;
using BoningerWorks.TextAdventure.Engine.Exceptions.Data;
using BoningerWorks.TextAdventure.Engine.Executables.Maps;
using BoningerWorks.TextAdventure.Engine.States;
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
			// Get template blueprints
			var templateBlueprints = gameBlueprint.Templates;
			// Check if template blueprints does not exist
			if (templateBlueprints == null)
			{
				// Throw error
				throw new ArgumentException("Templates cannot be null.", nameof(gameBlueprint));
			}
			// Set items
			Items = _CreateItems(gameBlueprint.Player);
			// Set commands
			Commands = _CreateCommands(Items, templateBlueprints.Commands);
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
				// Create item state
				var itemState = EntityState.CreateItem(item.Location, item.Active);
				// Add item state
				gameState.EntityStates.Add(item.Symbol, itemState);
			}
			// Return game state
			return gameState;
		}

		public List<string> Execute(GameState state, string input)
		{
			// Create responses
			var responses = new List<string>();
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
					// Run through item symbols
					for (int i = 0; i < command.ItemSymbols.Length; i++)
					{
						// Set command handler
						commandHandler = commandHandler.Next[commandMatch.ItemSymbolToItemMappings[command.ItemSymbols[i]].Symbol];
					}
					// Get action
					var action = commandHandler.Actions;



					responses.Add($"The {commandMatch.Command} command was triggered!");
					if (commandMatch.ItemSymbolToItemMappings.Count == 0)
					{
						responses.Add("No item symbols were returned.");
					}
					else
					{
						foreach (var keyValue in commandMatch.ItemSymbolToItemMappings)
						{
							responses.Add($"The {keyValue.Key} item symbol returned the {keyValue.Value} item.");
						}
					}
				}
				// Return responses
				return responses;
			}
			catch (GenericException<AmbiguousCommandItemMatchData> exception)
			{
				// Throw error
				throw;
			}
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

		private static Commands _CreateCommands(Items items, Dictionary<string, CommandTemplate> commandTemplates)
		{
			// Create command maps
			var commandMaps = Enumerable.Empty<CommandMap>()
				.Concat(items.SelectMany(i => i.CommandMaps))
				.ToImmutableList();
			// Return commands
			return new Commands(items, commandTemplates, commandMaps);
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Executables.Maps;
using BoningerWorks.TextAdventure.Engine.States;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Action
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Item> CommandItemSymbolToItemMappings { get; }

		public Action(Items items, Command command, CommandMap commandMap)
		{
			// Check if command does not match command map
			if (command.Symbol != commandMap.CommandSymbol)
			{
				// Throw error
				throw new ArgumentException($"Command ({command}) does not match command ({commandMap.CommandSymbol}) of command map.");
			}
			// Set command
			Command = command;
			// Check if command map has no command item symbol to item symbol mappings
			if (commandMap.CommandItemSymbolToItemSymbolMappings.Count == 0)
			{
				// Check if command has more than one item symbol
				if (Command.ItemSymbols.Length > 1)
				{
					// Throw error
					throw new ArgumentException($"Command map for command ({Command}) is not valid.", nameof(commandMap));
				}
			}
			else
			{
				// Check if command map does not have an item symbol for each command item symbol
				if (command.ItemSymbols.Any(cis => !commandMap.CommandItemSymbolToItemSymbolMappings.ContainsKey(cis)))
				{
					// Throw error
					throw new ArgumentException($"Command map for command ({Command}) is not valid.", nameof(commandMap));
				}
			}
			// Set command item symbol to item symbol mappings
			CommandItemSymbolToItemMappings = Command.ItemSymbols
				.Select
					(cis =>
					{
						// Try to get item symbol for command item symbol
						if (!commandMap.CommandItemSymbolToItemSymbolMappings.TryGetValue(cis, out var itemSymbol))
						{
							// Set item symbol to default item symbol
							itemSymbol = commandMap.ItemSymbolDefault;
						}
						// Check if item symbol does not exist
						if (itemSymbol == null)
						{
							// Create message
							var message = $"No mapping could be found in command map for command item symbol ({cis}) of command ({Command}).";
							// Throw error
							throw new InvalidOperationException(message);
						}
						// Check if item does not exist
						if (!items.Contains(itemSymbol))
						{
							// Throw error
							throw new InvalidOperationException($"No item with symbol ({itemSymbol}) could be found.");
						}
						// Return command item symbol to item mapping
						return KeyValuePair.Create(cis, items.Get(itemSymbol));
					})
				.ToImmutableDictionary();
		}

		public void Execute(GameState gameState)
		{



		}
	}
}

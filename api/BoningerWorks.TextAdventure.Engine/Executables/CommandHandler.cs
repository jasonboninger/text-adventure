using BoningerWorks.TextAdventure.Engine.Executables.Maps;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandHandler
	{
		private class CommandMapValidated
		{
			public ImmutableDictionary<Symbol, Item> CommandItemSymbolToItemMappings { get; }
			public ImmutableArray<Action> Actions { get; }

			public CommandMapValidated(Items items, Command command, CommandMap commandMap)
			{
				// Check if command map has no command item symbol to item symbol mappings
				if (commandMap.CommandItemSymbolToItemSymbolMappings.Count == 0)
				{
					// Check if command has more than one item symbol
					if (command.ItemSymbols.Length > 1)
					{
						// Throw error
						throw new ArgumentException("Command map is not valid with command.", nameof(commandMap));
					}
					// Check if command has one item symbol and command map does not have a default item symbol
					if (command.ItemSymbols.Length == 1 && commandMap.ItemSymbolDefault == null)
					{
						// Throw error
						throw new ArgumentException("Command map is not valid with command.", nameof(commandMap));
					}
				}
				// Set command item symbol to item symbol mappings
				CommandItemSymbolToItemMappings = command.ItemSymbols
					.Select
						(cis =>
						{
							// Try to get item symbol for command item symbol
							if (!commandMap.CommandItemSymbolToItemSymbolMappings.TryGetValue(cis, out var itemSymbol))
							{
								// Set item symbol
								itemSymbol = commandMap.ItemSymbolDefault;
							}
							// Check if item symbol does not exist
							if (itemSymbol == null)
							{
								// Create message
								var message = $"No mapping could be found in command map for command item symbol ({cis}) of command ({command}).";
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
				// Set actions
				Actions = commandMap.ActionMaps.Select(am => new Action(items, command, CommandItemSymbolToItemMappings, am)).ToImmutableArray();
			}
		}

		public ImmutableDictionary<Symbol, CommandHandler> Next { get; }
		public ImmutableArray<Action> Actions { get; }

		public CommandHandler(Items items, Command command, IEnumerable<CommandMap> commandMaps) 
		: this
		(
			command, 
			commandMaps.Select(cm => new CommandMapValidated(items, command, cm)), 
			depth: 0
		) 
		{ }
		private CommandHandler(Command command, IEnumerable<CommandMapValidated> commandMapsValidated, int depth)
		{
			// Check if item symbols exist at depth
			if (command.ItemSymbols.Length > depth)
			{
				// Set next
				Next = _CreateNext(command, commandMapsValidated, depth);
			}
			else
			{
				// Set actions
				Actions = _CreateActions(commandMapsValidated);
			}
		}

		private static ImmutableDictionary<Symbol, CommandHandler> _CreateNext
		(
			Command command,
			IEnumerable<CommandMapValidated> commandMapsValidated,
			int depth
		)
		{
			// Get next depth
			var depthNext = depth + 1;
			// Create next
			var next = commandMapsValidated
				.GroupBy
					(
						cmv =>
						{
							// Get item
							var item = cmv.CommandItemSymbolToItemMappings[command.ItemSymbols[depth]];
							// Return item symbol
							return item.Symbol;
						},
						(s, cmvs) => KeyValuePair.Create(s, new CommandHandler(command, cmvs, depthNext))
					)
				.ToImmutableDictionary();
			// Return next
			return next;
		}

		private static ImmutableArray<Action> _CreateActions(IEnumerable<CommandMapValidated> commandMapsValidated)
		{
			// Create actions
			var actions = commandMapsValidated.SelectMany(cmv => cmv.Actions).ToImmutableArray();
			// Return actions
			return actions;
		}
	}
}

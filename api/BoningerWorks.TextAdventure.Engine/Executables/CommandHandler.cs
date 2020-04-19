using BoningerWorks.TextAdventure.Engine.Maps;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandHandler
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, CommandHandler> Next { get; }
		public ImmutableArray<Action> Actions { get; }

		public CommandHandler(Items items, Command command, IEnumerable<CommandMap> commandMaps) 
		: this
		(
			command,
			commandMaps
				.Select
					(cm =>
					{
						// Check if command does not match command map
						if (command.Symbol != cm.CommandSymbol)
						{
							// Throw error
							throw new ArgumentException($"Command ({command}) does not match command ({cm.CommandSymbol}) of command map.");
						}
						// Create action
						var action = new Action(items, command, cm);
						// Return action
						return action;
					})
				.ToImmutableArray(),
			index: 0
		) 
		{ }
		private CommandHandler(Command command, ImmutableArray<Action> actions, int index)
		{
			// Set command
			Command = command;
			// Check if no more command item symbols
			if (index == Command.ItemSymbols.Length)
			{
				// Set actions
				Actions = actions.ToImmutableArray();
			}
			else
			{
				// Set next
				Next = _CreateNext(Command, actions, index);
			}
		}

		private static ImmutableDictionary<Symbol, CommandHandler> _CreateNext(Command command, ImmutableArray<Action> actions, int index)
		{
			// Get next index
			var indexNext = index + 1;
			// Create next
			var next = actions
				.GroupBy
					(
						a =>
						{
							// Get item
							var item = a.CommandItemSymbolToItemMappings[command.ItemSymbols[index]];
							// Return item symbol
							return item.Symbol;
						},
						(@is, @as) => KeyValuePair.Create(@is, new CommandHandler(command, @as.ToImmutableArray(), indexNext))
					)
				.ToImmutableDictionary();
			// Return next
			return next;
		}
	}
}

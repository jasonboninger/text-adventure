using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using BoningerWorks.TextAdventure.Engine.Exceptions;
using BoningerWorks.TextAdventure.Engine.Exceptions.Data;
using BoningerWorks.TextAdventure.Engine.Maps;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Commands : IReadOnlyList<Command>
	{
		public int Count => _commands.Length;
		public Command this[int index] => _commands[index];

		private readonly ImmutableArray<Command> _commands;
		private readonly IEnumerable<Command> _commandsEnumerable;
		private readonly ImmutableDictionary<Symbol, Command> _commandSymbolToCommandMappings;
		private readonly ImmutableDictionary<Symbol, CommandHandler> _commandSymbolToCommandHandlerMappings;

		public Commands(Items items, Dictionary<string, CommandTemplate> commandTemplates, ImmutableList<CommandMap> commandMaps)
		{
			// Check if command templates does not exist
			if (commandTemplates == null)
			{
				// Throw error
				throw new ArgumentException("Command templates cannot be null.", nameof(commandTemplates));
			}
			// Create commands
			var commands = commandTemplates
				.Select(kv => new Command(new Symbol(kv.Key), items, kv.Value))
				.OrderBy(c => c.Symbol.ToString())
				.ToImmutableArray();
			// Check if not all command symbols are unique
			if (commands.Select(c => c.Symbol).Distinct().Count() != commands.Length)
			{
				// Throw error
				throw new ArgumentException("Not all command symbols are unique.", nameof(commands));
			}
			// Set commands
			_commands = commands;
			// Set enumerable commands
			_commandsEnumerable = _commands;
			// Set command symbol to command mappings
			_commandSymbolToCommandMappings = _commands.ToImmutableDictionary(c => c.Symbol);
			// Set command symbol to command handler mappings
			_commandSymbolToCommandHandlerMappings = _CreateSymbolToCommandHandlerMappings(items, _commandSymbolToCommandMappings, commandMaps);
		}

		public IEnumerator<Command> GetEnumerator() => _commandsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _commandsEnumerable.GetEnumerator();

		public Command Get(Symbol symbol)
		{
			// Try to get command
			if (!TryGet(symbol, out var command))
			{
				// Throw error
				throw new ArgumentException($"No command with symbol ({symbol}) could be found.");
			}
			// Return command
			return command;
		}

		public bool TryGet(Symbol symbol, out Command command)
		{
			// Try to get command
			if (symbol != null && _commandSymbolToCommandMappings.TryGetValue(symbol, out command))
			{
				// Return succeeded
				return true;
			}
			// Set command
			command = null;
			// Return failed
			return false;
		}

		public CommandHandler GetHandler(Command command) => _commandSymbolToCommandHandlerMappings[command.Symbol];

		public bool TryCreateMatch(string input, out CommandMatch commandMatch)
		{
			// Run through commands
			for (int i = 0; i < _commands.Length; i++)
			{
				var command = _commands[i];
				// Try to match command
				try
				{
					// Try to match command
					if (command.TryMatchCommand(input, out var itemSymbolToItemsMappings))
					{
						// Set command match
						commandMatch = new CommandMatch(command, itemSymbolToItemsMappings);
						// Return succeeded
						return true;
					}
				}
				catch (GenericException<AmbiguousItemMatchData> exception)
				{
					// Get ambiguous item match data
					var ambiguousItemMatchData = exception.Data;
					// Throw error
					throw GenericException.Create(new AmbiguousCommandItemMatchData(command, ambiguousItemMatchData), exception);
				}
			}
			// Set command match
			commandMatch = null;
			// Return failed
			return false;
		}

		private static ImmutableDictionary<Symbol, CommandHandler> _CreateSymbolToCommandHandlerMappings
		(
			Items items,
			ImmutableDictionary<Symbol, Command> symbolToCommandMappings,
			ImmutableList<CommandMap> commandMaps
		)
		{
			// Create command symbol to command handler mappings
			var commandSymbolToCommandHandlerMappings = commandMaps
				.GroupBy
					(
						cm => cm.CommandSymbol,
						(cs, cms) =>
						{
							// Try to get command
							if (!symbolToCommandMappings.TryGetValue(cs, out var command))
							{
								// Throw error
								throw new InvalidOperationException($"No command with symbol ({cs}) could be found.");
							}
							// Create command handler
							var commandHandler = new CommandHandler(items, command, cms);
							// Return command symbol to command handler mapping
							return KeyValuePair.Create(cs, commandHandler);
						}
					)
				.ToImmutableDictionary();
			// Return command symbol to command handler mappings
			return commandSymbolToCommandHandlerMappings;
		}
	}
}

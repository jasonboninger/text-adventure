using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
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
		private readonly ImmutableDictionary<Symbol, Command> _symbolToCommandMappings;

		public Commands(Items items, ImmutableArray<CommandMap> commandMaps)
		{
			// Set commands
			_commands = commandMaps.Select(cm => new Command(items, cm)).OrderBy(c => c.Symbol.ToString()).ToImmutableArray();
			// Set enumerable commands
			_commandsEnumerable = _commands;
			// Set symbol to command mappings
			_symbolToCommandMappings = _commands.ToImmutableDictionary(c => c.Symbol);
		}

		public IEnumerator<Command> GetEnumerator() => _commandsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _commandsEnumerable.GetEnumerator();

		public Command Get(Symbol symbol)
		{
			// Try to get command
			var command = TryGet(symbol);
			// Check if command does not exist
			if (command == null)
			{
				// Throw error
				throw new ArgumentException($"No command with symbol ({symbol}) could be found.");
			}
			// Return command
			return command;
		}

		public Command? TryGet(Symbol symbol)
		{
			// Try to get command
			if (symbol != null && _symbolToCommandMappings.TryGetValue(symbol, out var command))
			{
				// Return command
				return command;
			}
			// Return no command
			return null;
		}

		public CommandMatch? TryGetMatch(string? input)
		{
			// Run through commands
			foreach (var command in _commands)
			{
				// Try to match command
				try
				{
					// Try to match command
					var commandItemToItemMappings = command.TryGetMatch(input);
					// Check if command item to item mappings exists
					if (commandItemToItemMappings != null)
					{
						// Create command match
						var commandMatch = new CommandMatch(command, commandItemToItemMappings);
						// Return command match
						return commandMatch;
					}
				}
				catch (GenericException<AmbiguousItemMatchError> exception)
				{
					// Get ambiguous item match error
					var ambiguousItemMatchError = exception.Error;
					// Check if ambiguous item match error exists
					if (ambiguousItemMatchError != null)
					{
						// Throw error
						throw GenericException.Create(new AmbiguousCommandItemMatchError(command, ambiguousItemMatchError), exception);
					}
					// Throw error
					throw;
				}
			}
			// Return no command match
			return null;
		}
	}
}

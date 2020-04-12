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

		public Commands(IEnumerable<Command> commands) : this(commands.ToImmutableArray()) { }
		public Commands(ImmutableArray<Command> commands)
		{
			// Check if not all commands exist
			if (commands.Any(c => c == null))
			{
				// Throw error
				throw new ArgumentException("No commands can be null", nameof(commands));
			}
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
		}

		public IEnumerator<Command> GetEnumerator() => _commandsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _commandsEnumerable.GetEnumerator();

		public bool TryMatchCommand(string input, out CommandMatch commandMatch)
		{
			// Run through commands
			for (int i = 0; i < _commands.Length; i++)
			{
				var command = _commands[i];
				// Try to match command
				if (command.TryMatchCommand(input, out var itemSymbolToItemsMappings))
				{
					// Set command match
					commandMatch = new CommandMatch(command, itemSymbolToItemsMappings);
					// Return succeeded
					return true;
				}
			}
			// Set command match
			commandMatch = null;
			// Return failed
			return false;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Commands : IReadOnlyList<Command>
	{
		public int Count => _commands.Count;
		public Command this[int index] => _commands[index];

		private readonly Group<Command> _commands;

		public Commands(Entities entities, ImmutableArray<CommandMap> commandMaps)
		{
			// Set commands
			_commands = new Group<Command>(commandMaps.Select(cm => new Command(entities, cm)).OrderBy(c => c.Symbol.ToString()));
		}

		IEnumerator IEnumerable.GetEnumerator() => _commands.GetEnumerator();
		public IEnumerator<Command> GetEnumerator() => _commands.GetEnumerator();

		public Command Get(Symbol symbol) => _commands.Get(symbol);

		public Command? TryGet(Symbol? symbol) => _commands.TryGet(symbol);

		public CommandMatch? TryGetMatch(string? input)
		{
			// Run through commands
			for (int i = 0; i < _commands.Count; i++)
			{
				var command = _commands[i];
				// Try to get match
				var match = command.TryGetMatch(input);
				// Check if match exists
				if (match != null)
				{
					// Return match
					return match;
				}
			}
			// Return no match
			return null;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandMatch
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Item> CommandItemToItemMappings { get; }

		public CommandMatch(Command command, ImmutableDictionary<Symbol, Item> commandItemToItemMappings)
		{
			// Set command
			Command = command;
			// Set command item to item mappings
			CommandItemToItemMappings = commandItemToItemMappings;
		}
	}
}

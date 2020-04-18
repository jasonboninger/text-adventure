using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandMatch
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, Item> ItemSymbolToItemMappings { get; }

		public CommandMatch(Command command, ImmutableDictionary<Symbol, Item> itemSymbolToItemMappings)
		{
			// Set command
			Command = command;
			// Set item symbol to item mappings
			ItemSymbolToItemMappings = itemSymbolToItemMappings;
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandMatch
	{
		public Command Command { get; }
		public ImmutableDictionary<Symbol, ImmutableArray<Item>> ItemSymbolToItemsMappings { get; }

		public CommandMatch(Command command, ImmutableDictionary<Symbol, ImmutableArray<Item>> itemSymbolToItemsMappings)
		{
			// Set command
			Command = command;
			// Set item symbol to items mappings
			ItemSymbolToItemsMappings = itemSymbolToItemsMappings;
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Command
	{
		public ImmutableArray<Symbol> ItemSymbols => _parser.ItemSymbols;

		public Symbol Symbol { get; }

		private readonly CommandParser _parser;

		public Command(Symbol symbol, Items items, CommandTemplate commandTemplate)
		{
			// Set symbol
			Symbol = symbol;
			// Set parser
			_parser = new CommandParser(items, commandTemplate);
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}

		public bool TryMatchCommand(string input, out ImmutableDictionary<Symbol, ImmutableArray<Item>> itemSymbolToItemsMappings)
		{
			// Try to match command
			return _parser.TryMatchCommand(input, out itemSymbolToItemsMappings);
		}
	}
}

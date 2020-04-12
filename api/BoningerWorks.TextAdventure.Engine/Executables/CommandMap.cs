using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandMap
	{
		public Symbol CommandSymbol { get; }
		public ImmutableDictionary<Symbol, Symbol> CommandItemSymbolToItemSymbolMappings { get; }
		public Symbol ItemSymbolDefault { get; }

		public CommandMap(Symbol commandSymbol, CommandBlueprint commandBlueprint, Symbol itemSymbolDefault = null)
		{
			// Set command symbol
			CommandSymbol = commandSymbol;
			// Check if command blueprint does not exist
			if (commandBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Command blueprint cannot be null.", nameof(commandBlueprint));
			}
			// Set command item symbol to item symbol mappings
			CommandItemSymbolToItemSymbolMappings = (commandBlueprint.Items ?? Enumerable.Empty<KeyValuePair<string, string>>())
				.ToImmutableDictionary(kv => new Symbol(kv.Key), kv => new Symbol(kv.Value));
			// Set default item symbol
			ItemSymbolDefault = itemSymbolDefault;
		}
	}
}

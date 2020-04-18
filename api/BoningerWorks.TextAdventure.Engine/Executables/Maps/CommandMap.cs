using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables.Maps
{
	public class CommandMap
	{
		public Symbol CommandSymbol { get; }
		public ImmutableDictionary<Symbol, Symbol> CommandItemSymbolToItemSymbolMappings { get; }
		public Symbol ItemSymbolDefault { get; }
		public ImmutableArray<ActionMap> ActionMaps { get; }

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
			CommandItemSymbolToItemSymbolMappings = _CreateCommandItemSymbolToItemSymbolMappings(commandBlueprint.Items);
			// Set action maps
			ActionMaps = _CreateActionMaps(commandBlueprint.Actions);
			// Set default item symbol
			ItemSymbolDefault = itemSymbolDefault;
		}

		private static ImmutableDictionary<Symbol, Symbol> _CreateCommandItemSymbolToItemSymbolMappings(Dictionary<string, string> items)
		{
			// Check if items does not exist
			if (items == null)
			{
				// Return no command item symbol to item symbol mappings
				return ImmutableDictionary<Symbol, Symbol>.Empty;
			}
			// Create command item symbol to item symbol mappings
			var commandItemSymbolToItemSymbolMappings = items.ToImmutableDictionary(kv => new Symbol(kv.Key), kv => new Symbol(kv.Value));
			// Return command item symbol to item symbol mappings
			return commandItemSymbolToItemSymbolMappings;
		}

		private static ImmutableArray<ActionMap> _CreateActionMaps(OneOrManyList<ActionBlueprint> actions)
		{
			// Check if actions does not exist or is empty
			if (actions == null || actions.Count == 0)
			{
				// Throw error
				throw new InvalidOperationException("Action blueprints cannot be null or emtpy.");
			}
			// Create action maps
			var actionMaps = actions.Select(a => new ActionMap(a)).ToImmutableArray();
			// Return action maps
			return actionMaps;
		}
	}
}

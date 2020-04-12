using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Blueprints.Items;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item
	{
		public Symbol Symbol { get; }
		public Name Name { get; }
		public Names Names { get; }
		public string RegularExpression { get; }
		public bool Active { get; }
		public ImmutableArray<CommandMap> CommandMaps { get; }

		public Item(Symbol symbol, ItemBlueprint itemBlueprint)
		{
			// Set symbol
			Symbol = symbol ?? throw new ArgumentException("Symbol cannot be null.", nameof(symbol));
			// Check if item blueprint does not exist
			if (itemBlueprint == null)
			{
				// Throw error
				throw new ArgumentException("Item blueprint cannot be null.");
			}
			// Check if item blueprint has no names
			if (itemBlueprint.Names == null || itemBlueprint.Names.Count == 0)
			{
				// Throw error
				throw new ArgumentException("Item blueprint must have at least one name.");
			}
			// Set names
			Names = new Names(itemBlueprint.Names.Select(n => new Name(n)));
			// Set name
			Name = Names.First();
			// Set regular expression
			RegularExpression = Names.RegularExpression;
			// Set active
			Active = itemBlueprint.Active ?? true;
			// Set command maps
			CommandMaps = _CreateCommandMaps(itemBlueprint.Commands);
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}

		private ImmutableArray<CommandMap> _CreateCommandMaps(Dictionary<string, CommandBlueprint> commandBlueprints)
		{
			// Check if command blueprints does not exist
			if (commandBlueprints == null)
			{
				// Return no command maps
				return ImmutableArray<CommandMap>.Empty;
			}
			// Create command maps
			var commandMaps = commandBlueprints.Select(kv => new CommandMap(new Symbol(kv.Key), kv.Value, Symbol)).ToImmutableArray();
			// Return command maps
			return commandMaps;
		}
	}
}

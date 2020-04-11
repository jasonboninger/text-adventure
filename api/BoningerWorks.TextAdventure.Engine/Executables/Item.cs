using BoningerWorks.TextAdventure.Engine.Blueprints.Items;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item
	{
		public string RegularExpression => Names.RegularExpression;

		public Symbol Symbol { get; }
		public Name Name { get; }
		public Names Names { get; }
		public bool Active { get; }

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
			// Set active
			Active = itemBlueprint.Active ?? true;
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}
	}
}

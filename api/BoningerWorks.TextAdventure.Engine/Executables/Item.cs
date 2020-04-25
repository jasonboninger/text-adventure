using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item
	{
		public Symbol Symbol { get; }
		public Symbol Location { get; }
		public Names Names { get; }
		public Name Name { get; }
		public bool? Active { get; }
		public string RegularExpression { get; }
		
		public Item(ItemMap itemMap)
		{
			// Set symbol
			Symbol = itemMap.ItemSymbol;
			// Set location
			Location = itemMap.LocationSymbol;
			// Set names
			Names = itemMap.ItemNames;
			// Set name
			Name = itemMap.ItemName;
			// Set active
			Active = itemMap.Active;
			// Set regular expression
			RegularExpression = Names.RegularExpression;
		}

		public override string ToString()
		{
			// Return string
			return Symbol.ToString();
		}
	}
}

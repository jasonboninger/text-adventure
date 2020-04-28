using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item
	{
		public static Symbol DatumActive { get; }  = new Symbol("ACTIVE");
		public static Symbol DatumLocation { get; } = new Symbol("LOCATION");

		public Symbol Symbol { get; }
		public Symbol Location { get; }
		public Names Names { get; }
		public Name Name { get; }
		public bool? Active { get; }
		public string RegularExpression { get; }
		
		public Item(Player player, Areas areas, ItemMap itemMap)
		{
			// Set symbol
			Symbol = itemMap.ItemSymbol;
			// Set location
			Location = itemMap.LocationSymbol;
			// Check if location does not exist
			if (Location != player.Symbol && !areas.Contains(Location))
			{
				// Throw error
				throw new ValidationError($"Item location ({Location}) could not be found.");
			}
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

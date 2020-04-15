using BoningerWorks.TextAdventure.Engine.Utilities;

namespace BoningerWorks.TextAdventure.Engine.States.Data
{
	public class ItemData
	{
		public static ItemData Create(Symbol location, bool active)
		{
			// Create item data
			var itemData = new ItemData
			{
				Location = location,
				Active = active
			};
			// Return item data
			return itemData;
		}

		public Symbol Location { get; set; }
		public bool Active { get; set; }
	}
}

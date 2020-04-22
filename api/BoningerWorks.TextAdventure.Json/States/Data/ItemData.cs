using BoningerWorks.TextAdventure.Core.Utilities;
using System;

namespace BoningerWorks.TextAdventure.Json.States.Data
{
	public class ItemData
	{
		public static ItemData Create(Symbol location, bool active)
		{
			// Create item data
			var itemData = new ItemData
			{
				Location = location ?? throw new ArgumentException("Location cannot be null.", nameof(location)),
				Active = active
			};
			// Return item data
			return itemData;
		}

		public Symbol Location { get; set; }
		public bool Active { get; set; }
	}
}

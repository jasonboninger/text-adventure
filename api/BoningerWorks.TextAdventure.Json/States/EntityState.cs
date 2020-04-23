using BoningerWorks.TextAdventure.Json.States.Data;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class EntityState
	{
		public static EntityState CreateGlobal()
		{
			// Create global state
			var globalState = _Create();
			// Set global data
			globalState.GlobalData = GlobalData.Create();
			// Return global state
			return globalState;
		}

		public static EntityState CreatePlayer()
		{
			// Create player state
			var playerState = _Create();
			// Set player data
			playerState.PlayerData = PlayerData.Create();
			// Return player state
			return playerState;
		}

		public static EntityState CreateItem(ItemData itemData)
		{
			// Create item state
			var itemState = _Create();
			// Set item data
			itemState.ItemData = itemData ?? throw new ArgumentException("Item data cannot be null.", nameof(itemData));
			// Return item state
			return itemState;
		}

		private static EntityState _Create()
		{
			// Create entity state
			var entityState = new EntityState
			{
				CustomData = new Dictionary<string, string?>()
			};
			// Return entity state
			return entityState;
		}

		public GlobalData? GlobalData { get; set; }
		public PlayerData? PlayerData { get; set; }
		public ItemData? ItemData { get; set; }
		public Dictionary<string, string?>? CustomData { get; set; }
	}
}

﻿using BoningerWorks.TextAdventure.Engine.States.Data;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.States
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

		public static EntityState CreateItem(Symbol location, bool active)
		{
			// Create item state
			var itemState = _Create();
			// Set item data
			itemState.ItemData = ItemData.Create(location, active);
			// Return item state
			return itemState;
		}

		private static EntityState _Create()
		{
			// Create entity state
			var entityState = new EntityState
			{
				CustomData = new Dictionary<string, string>()
			};
			// Return entity state
			return entityState;
		}

		public GlobalData GlobalData { get; set; }
		public PlayerData PlayerData { get; set; }
		public ItemData ItemData { get; set; }
		public Dictionary<string, string> CustomData { get; set; }
	}
}
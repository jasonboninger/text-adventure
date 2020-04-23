using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class GameState
	{
		public static GameState Create()
		{
			// Create game state
			var gameState = new GameState
			{
				EntityStates = new Dictionary<Symbol, EntityState>()
			};
			// Return game state
			return gameState;
		}

		public Dictionary<Symbol, EntityState>? EntityStates { get; set; }
	}
}

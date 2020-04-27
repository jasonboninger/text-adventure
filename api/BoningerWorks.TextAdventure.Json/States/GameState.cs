using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class GameState
	{
		public ImmutableDictionary<Symbol, EntityState> EntityStates { get; }

		public GameState(ImmutableDictionary<Symbol, EntityState> entityStates)
		{
			// Set entity states
			EntityStates = entityStates ?? ImmutableDictionary<Symbol, EntityState>.Empty;
		}

		public GameState UpdateEntityState(Symbol entitySymbol, EntityState entityState)
		{
			// Check if entity state does not exist
			if (!EntityStates.ContainsKey(entitySymbol))
			{
				// Throw error
				throw new ArgumentException($"Entity state for entity symbol ({entitySymbol}) could not be found.", nameof(entitySymbol));
			}
			// Set entity state
			var entitySymbolToEntityStateMappings = EntityStates.SetItem(entitySymbol, entityState);
			// Create game state
			var gameState = new GameState(entitySymbolToEntityStateMappings);
			// Return game state
			return gameState;
		}
	}
}

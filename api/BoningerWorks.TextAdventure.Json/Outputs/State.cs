using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class State
	{
		public ImmutableDictionary<Symbol, Entity> EntityStates { get; }

		public State(ImmutableDictionary<Symbol, Entity> entityStates)
		{
			// Set entity states
			EntityStates = entityStates ?? ImmutableDictionary<Symbol, Entity>.Empty;
		}

		public State UpdateEntityState(Symbol entitySymbol, Entity entityState)
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
			var gameState = new State(entitySymbolToEntityStateMappings);
			// Return game state
			return gameState;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class State
	{
		public ImmutableDictionary<Symbol, Entity> Entities { get; }

		public State(ImmutableDictionary<Symbol, Entity> entityStates)
		{
			// Set entities
			Entities = entityStates ?? ImmutableDictionary<Symbol, Entity>.Empty;
		}

		public State UpdateEntity(Symbol entitySymbol, Entity entityState)
		{
			// Check if entity does not exist
			if (!Entities.ContainsKey(entitySymbol))
			{
				// Throw error
				throw new ArgumentException($"Entity with symbol ({entitySymbol}) could not be found.", nameof(entitySymbol));
			}
			// Set entity
			var entities = Entities.SetItem(entitySymbol, entityState);
			// Create state
			var state = new State(entities);
			// Return state
			return state;
		}
	}
}

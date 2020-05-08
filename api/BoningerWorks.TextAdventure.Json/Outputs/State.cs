using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class State
	{
		public ImmutableDictionary<Symbol, Entity> Entities { get; }
		public bool Complete { get; }

		public State(ImmutableDictionary<Symbol, Entity> entityStates, bool complete)
		{
			// Set entities
			Entities = entityStates ?? ImmutableDictionary<Symbol, Entity>.Empty;
			// Set complete
			Complete = complete;
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
			var state = new State(entities, Complete);
			// Return state
			return state;
		}

		public State UpdateComplete(bool complete)
		{
			// Create state
			var state = new State(Entities, complete);
			// Return state
			return state;
		}
	}
}

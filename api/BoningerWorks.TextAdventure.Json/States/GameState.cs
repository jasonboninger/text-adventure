using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class GameState
	{
		public ImmutableDictionary<Symbol, EntityState> EntityStates { get; }

		public GameState(ImmutableList<EntityState>? entityStates)
		{
			// Set entity states
			EntityStates = entityStates?.ToImmutableDictionary(es => es.Id) ?? ImmutableDictionary<Symbol, EntityState>.Empty;
		}
	}
}

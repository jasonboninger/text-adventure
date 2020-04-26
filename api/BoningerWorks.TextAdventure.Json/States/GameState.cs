using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class GameState
	{
		public ImmutableDictionary<Symbol, EntityState> EntityStates { get; }

		public GameState(ImmutableDictionary<Symbol, EntityState>? entityStates)
		{
			// Set entity states
			EntityStates = entityStates ?? ImmutableDictionary<Symbol, EntityState>.Empty;
		}
	}
}

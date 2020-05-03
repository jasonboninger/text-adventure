using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Entity
	{
		public ImmutableDictionary<Symbol, string> Data { get; }

		public Entity(ImmutableDictionary<Symbol, string>? data = null)
		{
			// Set data
			Data = data ?? ImmutableDictionary<Symbol, string>.Empty;
		}

		public Entity UpdateData(Symbol symbol, string value)
		{
			// Set data
			var data = Data.SetItem(symbol, value);
			// Create entity
			var entity = new Entity(data);
			// Return entity
			return entity;
		}
	}
}

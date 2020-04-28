using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Entity
	{
		public ImmutableDictionary<Symbol, Symbol> Data { get; }
		public ImmutableDictionary<Symbol, string> CustomData { get; }

		public Entity(ImmutableDictionary<Symbol, Symbol>? data, ImmutableDictionary<Symbol, string>? customData)
		{
			// Set data
			Data = data ?? ImmutableDictionary<Symbol, Symbol>.Empty;
			// Set custom data
			CustomData = customData ?? ImmutableDictionary<Symbol, string>.Empty;
		}

		public Entity UpdateData(Symbol symbol, Symbol value)
		{
			// Check if data does not exist
			if (!Data.ContainsKey(symbol))
			{
				// Throw error
				throw new ArgumentException($"Data ({symbol}) for entity could not be found.", nameof(symbol));
			}
			// Set data
			var data = Data.SetItem(symbol, value);
			// Create entity
			var entity = new Entity(data, CustomData);
			// Return entity
			return entity;
		}

		public Entity UpdateCustomData(Symbol symbol, string value)
		{
			// Set custom data
			var customData = CustomData.SetItem(symbol, value);
			// Create entity
			var entity = new Entity(Data, customData);
			// Return entity
			return entity;
		}
	}
}

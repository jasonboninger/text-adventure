using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class EntityState
	{
		public ImmutableDictionary<Symbol, Symbol> Data { get; }
		public ImmutableDictionary<Symbol, string> CustomData { get; }

		public EntityState(ImmutableDictionary<Symbol, Symbol>? data, ImmutableDictionary<Symbol, string>? customData)
		{
			// Set data
			Data = data ?? ImmutableDictionary<Symbol, Symbol>.Empty;
			// Set custom data
			CustomData = customData ?? ImmutableDictionary<Symbol, string>.Empty;
		}

		public EntityState UpdateData(Symbol symbol, Symbol value)
		{
			// Check if data does not exist
			if (!Data.ContainsKey(symbol))
			{
				// Throw error
				throw new ArgumentException($"Data ({symbol}) for entity could not be found.", nameof(symbol));
			}
			// Set data
			var data = Data.SetItem(symbol, value);
			// Create entity state
			var entityState = new EntityState(data, CustomData);
			// Return entity state
			return entityState;
		}

		public EntityState UpdateCustomData(Symbol symbol, string value)
		{
			// Set custom data
			var customData = CustomData.SetItem(symbol, value);
			// Create entity state
			var entityState = new EntityState(Data, customData);
			// Return entity state
			return entityState;
		}
	}
}

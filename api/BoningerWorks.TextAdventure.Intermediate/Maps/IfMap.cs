using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class IfMap<TMap>
	{
		public static IfMap<TMap> Create<TValue>(If<TValue>? @if, Func<TValue, TMap> converter)
		{
			// Check if if does not exist
			if (@if == null)
			{
				// Throw error
				throw new ValidationError("If cannot be null.");
			}
			// Create condition map
			var conditionMap = new ConditionMap(@if.Condition);
			// Check if both true values and false values do not exist
			if ((@if.ValuesTrue == null || @if.ValuesTrue.Count == 0) && (@if.ValuesFalse == null || @if.ValuesFalse.Count == 0))
			{
				// Throw error
				throw new ValidationError("Both true values and false values cannot be null or empty.");
			}
			// Create true maps
			var mapsTrue = @if.ValuesTrue?.Select(converter).ToImmutableArray();
			// Create false maps
			var mapsFalse = @if.ValuesFalse?.Select(converter).ToImmutableArray();
			// Return if map
			return new IfMap<TMap>(conditionMap, mapsTrue, mapsFalse);
		}

		public ConditionMap ConditionMap { get; }
		public ImmutableArray<TMap>? MapsTrue { get; }
		public ImmutableArray<TMap>? MapsFalse { get; }

		private IfMap(ConditionMap conditionMap, ImmutableArray<TMap>? mapsTrue, ImmutableArray<TMap>? mapsFalse)
		{
			// Set condition map
			ConditionMap = conditionMap;
			// Set true maps
			MapsTrue = mapsTrue;
			// Set false maps
			MapsFalse = mapsFalse;
		}
	}
}

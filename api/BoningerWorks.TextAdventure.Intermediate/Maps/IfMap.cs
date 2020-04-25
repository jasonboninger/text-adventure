using BoningerWorks.TextAdventure.Intermediate.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class IfMap<TMap>
	{
		public ConditionMap ConditionMap { get; }
		public ImmutableArray<TMap>? MapsTrue { get; }
		public ImmutableArray<TMap>? MapsFalse { get; }

		internal IfMap(ConditionMap conditionMap, ImmutableArray<TMap>? mapsTrue, ImmutableArray<TMap>? mapsFalse)
		{
			// Set condition map
			ConditionMap = conditionMap;
			// Check if both true maps and false maps do not exist
			if ((!mapsTrue.HasValue || mapsTrue.Value.Length == 0) && (!mapsFalse.HasValue || mapsFalse.Value.Length == 0))
			{
				// Throw error
				throw new ValidationError("Both true maps and false maps cannot be null or empty.");
			}
			// Set true maps
			MapsTrue = mapsTrue.HasValue && mapsTrue.Value.Length > 0 ? mapsTrue.Value : (ImmutableArray<TMap>?)null;
			// Set false maps
			MapsFalse = mapsFalse.HasValue && mapsFalse.Value.Length > 0 ? mapsFalse.Value : (ImmutableArray<TMap>?)null;
		}
	}
}

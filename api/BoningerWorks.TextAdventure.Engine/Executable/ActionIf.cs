using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionIf
	{
		public static Func<State, IEnumerable<TOutput>> Create<TMap, TOutput>
		(
			Func<Id, Id> replacer,
			Entities entities,
			IfMap<TMap> ifMap,
			Func<TMap, TOutput> create
		)
		{
			// Return action
			return Create(replacer, entities, ifMap, m => create(m).ToEnumerable());
		}
		public static Func<State, IEnumerable<TOutput>> Create<TMap, TOutput>
		(
			Func<Id, Id> replacer,
			Entities entities,
			IfMap<TMap> ifMap,
			Func<TMap, IEnumerable<TOutput>> create
		)
		{
			// Create condition action
			var actionCondition = ActionCondition.Create(replacer, entities, ifMap.ConditionMap);
			// Get true maps
			var mapsTrue = ifMap.MapsTrue;
			// Create true actions
			var actionsTrue = mapsTrue.HasValue ? mapsTrue.SelectMany(create) : Enumerable.Empty<TOutput>();
			// Test true actions
			_ = actionsTrue.ToList();
			// Get false maps
			var mapsFalse = ifMap.MapsFalse;
			// Create false actions
			var actionsFalse = mapsFalse.HasValue ? mapsFalse.SelectMany(create) : Enumerable.Empty<TOutput>();
			// Test false actions
			_ = actionsFalse.ToList();
			// Return action
			return s => actionCondition(s) ? actionsTrue : actionsFalse;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionIf<TOutput>
	{
		public static Func<State, ImmutableArray<TOutput>> Create<TMap>
		(
			Func<Id, Id> replacer,
			Entities entities, 
			IfMap<TMap> ifMap,
			Func<TMap, IEnumerable<TOutput>> create
		)
		{
			// Create condition action
			var actionCondition = ActionCondition.Create(replacer, entities, ifMap.ConditionMap);
			// Create true actions
			var actionsTrue = ifMap.MapsTrue.GetValueOrDefault(ImmutableArray<TMap>.Empty).SelectMany(create).ToImmutableArray();
			// Create false actions
			var actionsFalse = ifMap.MapsFalse.GetValueOrDefault(ImmutableArray<TMap>.Empty).SelectMany(create).ToImmutableArray();
			// Return action
			return state => actionCondition(state) ? actionsTrue : actionsFalse;
		}
	}
}

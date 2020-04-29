using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionIf
	{
		public static Action<ResultBuilder> Create(Entities entities, Items items, IfMap<ActionMap> ifMap)
		{
			// Create condition action
			var actionCondition = ActionCondition.Create(entities, ifMap.ConditionMap);
			// Create true actions
			var actionsTrue = ifMap.MapsTrue.GetValueOrDefault(ImmutableArray<ActionMap>.Empty)
				.SelectMany(am => Action.Create(entities, items, am))
				.ToImmutableArray();
			// Create false actions
			var actionsFalse = ifMap.MapsFalse.GetValueOrDefault(ImmutableArray<ActionMap>.Empty)
				.SelectMany(am => Action.Create(entities, items, am))
				.ToImmutableArray();
			// Return action
			return result =>
			{
				// Create actions
				var actions = actionCondition(result.State) ? actionsTrue : actionsFalse;
				// Run through actions
				for (int i = 0; i < actions.Length; i++)
				{
					// Execute action
					actions[i](result);
				}
			};
		}
	}
}

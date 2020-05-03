using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class Action
	{
		public static IEnumerable<Action<ResultBuilder>> Create(Entities entities, Items items, ActionMap actionMap)
		{
			// Check if if map exists
			if (actionMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf<Action<ResultBuilder>>.Create(entities, actionMap.IfMap, am => Create(entities, items, am));
				// Create action
				Action<ResultBuilder> action = result =>
				{
					// Get actions
					var actions = actionIf(result.State);
					// Run through actions
					for (int i = 0; i < actions.Length; i++)
					{
						// Execute action
						actions[i](result);
					}
				};
				// Return action
				return action.ToEnumerable();
			}
			// Check if message maps exist
			if (actionMap.MessageMaps.HasValue)
			{
				// Return message actions
				return actionMap.MessageMaps.Value.Select(mm => ActionMessage.Create(entities, mm));
			}
			// Check if change maps exist
			if (actionMap.ChangeMaps.HasValue)
			{
				// Return change actions
				return actionMap.ChangeMaps.Value.Select(cm => ActionChange.Create(entities, cm));
			}
			// Check if trigger maps exist
			if (actionMap.TriggerMaps.HasValue)
			{



				return Enumerable.Empty<Action<ResultBuilder>>();



			}
			// Throw error
			throw new InvalidOperationException("Action map could not be parsed.");
		}
	}
}

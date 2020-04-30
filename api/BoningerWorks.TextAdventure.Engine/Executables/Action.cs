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
				// Return if action
				return ActionIf.Create(entities, items, actionMap.IfMap).ToEnumerable();
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

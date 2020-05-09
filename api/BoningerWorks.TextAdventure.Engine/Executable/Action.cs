using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class Action
	{
		public static IEnumerable<Action<ResultBuilder>> Create
		(
			Func<Id, Id> replacer,
			Triggers? triggers,
			Entities entities,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			ActionMap actionMap
		)
		{
			// Check if iterator maps exists
			if (actionMap.IteratorMaps.HasValue)
			{
				// Create iterator action
				var actionIterator = ActionIterator.Create
					(
						replacer,
						entities,
						actionMap.IteratorMaps.Value,
						(r, am) => Create(r, triggers, entities, commands, reactionPaths, reactionPath, am)
					);
				// Create action
				Action<ResultBuilder> action = r =>
				{
					// Execute actions
					foreach (var action in actionIterator()) action(r);
				};
				// Return action
				return action.ToEnumerable();
			}
			// Check if if map exists
			if (actionMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf.Create
					(
						replacer, 
						entities, 
						actionMap.IfMap,
						am => Create(replacer, triggers, entities, commands, reactionPaths, reactionPath, am)
					);
				// Create action
				Action<ResultBuilder> action = r =>
				{
					// Execute actions
					foreach (var action in actionIf(r.State)) action(r);
				};
				// Return action
				return action.ToEnumerable();
			}
			// Check if message maps exist
			if (actionMap.MessageMaps.HasValue)
			{
				// Return message actions
				return actionMap.MessageMaps.Value.Select(mm => ActionMessage.Create(replacer, entities, mm));
			}
			// Check if change maps exist
			if (actionMap.ChangeMaps.HasValue)
			{
				// Return change actions
				return actionMap.ChangeMaps.Value.Select(cm => ActionChange.Create(replacer, entities, cm));
			}
			// Check if trigger maps exist
			if (actionMap.TriggerMaps.HasValue)
			{
				// Return trigger actions
				return actionMap.TriggerMaps.Value.Select(tm => ActionTrigger.Create(triggers, commands, reactionPaths, reactionPath, tm));
			}
			// Check if special exists
			if (actionMap.Special.HasValue)
			{
				// Return special action
				return ActionSpecial.Create(actionMap.Special.Value).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Action map could not be parsed.");
		}
	}
}

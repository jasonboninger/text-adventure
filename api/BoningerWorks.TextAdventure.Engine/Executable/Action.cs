using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class Action
	{
		public static Action<ResultBuilder> Create
		(
			Triggers? triggers,
			Entities entities,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			ImmutableArray<ActionMap> actionMaps,
			Func<Id, Id>? replacer = null,
			ImmutableList<IEntity>? entitiesAmbiguous = null
		)
		{
			// Create actions
			var actions = actionMaps.Select(am =>
			{
				// Create action
				var action = _Create
					(
						replacer ?? (id => id),
						triggers,
						entities,
						entitiesAmbiguous ?? ImmutableList<IEntity>.Empty,
						commands,
						reactionPaths,
						reactionPath,
						am
					);
				// Return action
				return action;
			});
			// Test actions
			_ = actions.ToList();
			// Return action
			return r =>
			{
				// Execute actions
				foreach (var action in actions) action(r);
			};
		}
		private static Action<ResultBuilder> _Create
		(
			Func<Id, Id> replacer,
			Triggers? triggers,
			Entities entities,
			ImmutableList<IEntity> entitiesAmbiguous,
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
						(r, am) => _Create(r, triggers, entities, entitiesAmbiguous, commands, reactionPaths, reactionPath, am)
					);
				// Return action
				return r =>
				{
					// Execute iterator action
					foreach (var action in actionIterator()) action(r);
				};
			}
			// Check if if map exists
			if (actionMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf.Create
					(
						replacer,
						entities,
						entitiesAmbiguous,
						actionMap.IfMap,
						am => _Create(replacer, triggers, entities, entitiesAmbiguous, commands, reactionPaths, reactionPath, am)
					);
				// Return action
				return r =>
				{
					// Execute if action
					foreach (var action in actionIf(r.State)) action(r);
				};
			}
			// Check if message maps exist
			if (actionMap.MessageMaps.HasValue)
			{
				// Get message maps
				var messageMaps = actionMap.MessageMaps.Value;
				// Create message actions
				var actionsMessage = messageMaps.Select(mm => ActionMessage.Create(replacer, entities, entitiesAmbiguous, mm));
				// Test message actions
				_ = actionsMessage.ToList();
				// Return action
				return r =>
				{
					// Execute message actions
					foreach (var actionMessage in actionsMessage) actionMessage(r);
				};
			}
			// Check if change maps exist
			if (actionMap.ChangeMaps.HasValue)
			{
				// Get change maps
				var changeMaps = actionMap.ChangeMaps.Value;
				// Create change actions
				var actionsChange = changeMaps.Select(cm => ActionChange.Create(replacer, entities, entitiesAmbiguous, cm));
				// Test change actions
				_ = actionsChange.ToList();
				// Return action
				return r =>
				{
					// Execute change actions
					foreach (var actionChange in actionsChange) actionChange(r);
				};
			}
			// Check if trigger maps exist
			if (actionMap.TriggerMaps.HasValue)
			{
				// Get trigger maps
				var triggerMaps = actionMap.TriggerMaps.Value;
				// Create trigger actions
				var actionsTrigger = triggerMaps.Select(tm => ActionTrigger.Create(triggers, commands, reactionPaths, reactionPath, tm));
				// Test trigger actions
				_ = actionsTrigger.ToList();
				// Return trigger actions
				return r =>
				{
					// Execute trigger actions
					foreach (var actionTrigger in actionsTrigger) actionTrigger(r);
				};
			}
			// Check if special exists
			if (actionMap.Special.HasValue)
			{
				// Create special action
				var actionSpecial = ActionSpecial.Create(actionMap.Special.Value);
				// Return action
				return r => actionSpecial(r);
			}
			// Throw error
			throw new InvalidOperationException("Action map could not be parsed.");
		}
	}
}

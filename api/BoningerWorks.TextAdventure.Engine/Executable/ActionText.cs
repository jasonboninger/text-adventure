using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionText
	{
		public static Func<State, IEnumerable<Text>> Create
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableList<IEntity> entitiesAmbiguous,
			TextMap textMap
		)
		{
			// Check if iterator maps exists
			if (textMap.IteratorMaps.HasValue)
			{
				// Create iterator action
				var actionIterator = ActionIterator.Create
					(
						replacer,
						entities,
						textMap.IteratorMaps.Value,
						(r, lm) => Create(r, entities, entitiesAmbiguous, lm)
					);
				// Create action
				IEnumerable<Text> action(State state)
				{
					// Run through actions
					foreach (var action in actionIterator())
					{
						// Run through texts
						foreach (var text in action(state))
						{
							// Return text
							yield return text;
						}
					}
				}
				// Return action
				return action;
			}
			// Check if if map exists
			if (textMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf.Create
					(
						replacer,
						entities,
						entitiesAmbiguous,
						textMap.IfMap, tm => Create(replacer, entities, entitiesAmbiguous, tm)
					);
				// Create action
				IEnumerable<Text> action(State state)
				{
					// Run through actions
					foreach (var action in actionIf(state))
					{
						// Run through texts
						foreach (var text in action(state))
						{
							// Return text
							yield return text;
						}
					}
				}
				// Return action
				return action;
			}
			// Check if inlined map exists
			if (textMap.InlinedMap != null)
			{
				// Create inlined text action
				var actionTextInlined = ActionTextInlined.Create(replacer, entities, entitiesAmbiguous, textMap.InlinedMap);
				// Return action
				return s => actionTextInlined(s).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Text map could not be parsed.");
		}
	}
}

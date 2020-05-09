using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionLine
	{
		public static Func<State, IEnumerable<Line>> Create(Func<Id, Id> replacer, Entities entities, LineMap lineMap)
		{
			// Check if iterator maps exists
			if (lineMap.IteratorMaps.HasValue)
			{
				// Create iterator action
				var actionIterator = ActionIterator.Create(replacer, entities, lineMap.IteratorMaps.Value, (r, lm) => Create(r, entities, lm));
				// Create action
				IEnumerable<Line> action(State state)
				{
					// Run through actions
					foreach (var action in actionIterator())
					{
						// Run through lines
						foreach (var line in action(state))
						{
							// Return line
							yield return line;
						}
					}
				}
				// Return action
				return action;
			}
			// Check if if map exists
			if (lineMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf.Create(replacer, entities, lineMap.IfMap, lm => Create(replacer, entities, lm).ToEnumerable());
				// Create action
				IEnumerable<Line> action(State state)
				{
					// Run through actions
					foreach (var action in actionIf(state))
					{
						// Run through lines
						foreach (var line in action(state))
						{
							// Return line
							yield return line;
						}
					}
				}
				// Return action
				return action;
			}
			// Check if special map exists
			if (lineMap.SpecialMap != null)
			{
				// Create special line action
				var actionLineSpecial = ActionLineSpecial.Create(lineMap.SpecialMap);
				// Return special line execute
				return s => actionLineSpecial(s).ToEnumerable();
			}
			// Check if inlined map exists
			if (lineMap.InlinedMap != null)
			{
				// Create inlined line action
				var actionLineInlined = ActionLineInlined.Create(replacer, entities, lineMap.InlinedMap);
				// Return action
				return s => actionLineInlined(s).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Line map could not be parsed.");
		}
	}
}

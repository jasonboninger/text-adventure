using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionLine
	{
		public static Func<State, IEnumerable<Line>> Create(Func<Id, Id> replacer, Entities entities, LineMap lineMap)
		{
			// Check if if map exists
			if (lineMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf<Func<State, IEnumerable<Line>>>.Create
					(
						replacer,
						entities, 
						lineMap.IfMap, 
						lm => Create(replacer, entities, lm).ToEnumerable()
					);
				// Return action
				return state => actionIf(state).SelectMany(a => a(state));
			}
			// Check if special map exists
			if (lineMap.SpecialMap != null)
			{
				// Create special line action
				var actionLineSpecial = ActionLineSpecial.Create(lineMap.SpecialMap);
				// Return special line execute
				return state => actionLineSpecial(state).ToEnumerable();
			}
			// Check if inlined map exists
			if (lineMap.InlinedMap != null)
			{
				// Create inlined line action
				var actionLineInlined = ActionLineInlined.Create(replacer, entities, lineMap.InlinedMap);
				// Return action
				return state => actionLineInlined(state).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Line map could not be parsed.");
		}
	}
}

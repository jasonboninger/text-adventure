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
			// Check if iterator maps exists
			if (lineMap.IteratorMaps.HasValue)
			{
				// Create line actions
				var actionsLine = lineMap.IteratorMaps.Value
					.SelectMany(im => ActionIterator.Create(replacer, entities, im, (r, lm) => Create(r, entities, lm).ToEnumerable()))
					.ToList();
				// Return action
				return s => actionsLine.SelectMany(a => a(s));
			}
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
				return s => actionIf(s).SelectMany(a => a(s));
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

﻿using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionText
	{
		public static Func<State, IEnumerable<Text>> Create(Func<Id, Id> replacer, Entities entities, TextMap textMap)
		{
			// Check if iterator maps exists
			if (textMap.IteratorMaps.HasValue)
			{
				// Create text actions
				var actionsText = textMap.IteratorMaps.Value
					.SelectMany(im => ActionIterator.Create(replacer, entities, im, (r, lm) => Create(r, entities, lm).ToEnumerable()))
					.ToList();
				// Return action
				return s => actionsText.SelectMany(a => a(s));
			}
			// Check if if map exists
			if (textMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf.Create(replacer, entities, textMap.IfMap, tm => Create(replacer, entities, tm));
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
				var actionTextInlined = ActionTextInlined.Create(replacer, entities, textMap.InlinedMap);
				// Return action
				return s => actionTextInlined(s).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Text map could not be parsed.");
		}
	}
}

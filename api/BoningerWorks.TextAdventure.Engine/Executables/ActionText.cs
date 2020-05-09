using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionText
	{
		public static Func<State, IEnumerable<Text>> Create(Func<Id, Id> replacer, Entities entities, TextMap textMap)
		{
			// Check if if map exists
			if (textMap.IfMap != null)
			{
				// Create if action
				var actionIf = ActionIf<Func<State, IEnumerable<Text>>>.Create
					(
						replacer,
						entities,
						textMap.IfMap, 
						tm => Create(replacer, entities, tm).ToEnumerable()
					);
				// Return action
				return state => actionIf(state).SelectMany(a => a(state));
			}
			// Check if inlined map exists
			if (textMap.InlinedMap != null)
			{
				// Create inlined text action
				var actionTextInlined = ActionTextInlined.Create(replacer, entities, textMap.InlinedMap);
				// Return action
				return state => actionTextInlined(state).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Text map could not be parsed.");
		}
	}
}

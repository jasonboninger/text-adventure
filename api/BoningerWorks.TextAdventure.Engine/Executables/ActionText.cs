using BoningerWorks.TextAdventure.Core.Extensions;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionText
	{
		public static Func<State, IEnumerable<Text>> Create(Entities entities, TextMap textMap)
		{
			// Check if if map exists
			if (textMap.IfMap != null)
			{



				// Throw error
				throw new InvalidOperationException("Text map if is not implemented.");



			}
			// Check if inlined map exists
			if (textMap.InlinedMap != null)
			{
				// Create inlined text action
				var actionTextInlined = ActionTextInlined.Create(entities, textMap.InlinedMap);
				// Return action
				return state => actionTextInlined(state).ToEnumerable();
			}
			// Throw error
			throw new InvalidOperationException("Text map could not be parsed.");
		}
	}
}

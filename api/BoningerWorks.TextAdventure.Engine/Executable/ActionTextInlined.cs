using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionTextInlined
	{
		public static Func<State, Text> Create(Func<Id, Id> replacer, Entities entities, TextInlinedMap textInlinedMap)
		{
			// Create replace
			var replace = ActionReplace.Create(replacer, entities, textInlinedMap.Value);
			// Return action
			return state =>
			{
				// Create value
				var value = replace(state);
				// Return text
				return new Text(value);
			};
		}
	}
}

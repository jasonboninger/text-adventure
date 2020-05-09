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
			// Create replace action
			var actionReplace = ActionReplace.Create(replacer, entities, textInlinedMap.Value);
			// Return action
			return s =>
			{
				// Create value
				var value = actionReplace(s);
				// Create text
				var text = new Text(value);
				// Return text
				return text;
			};
		}
	}
}

using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionTextInlined
	{
		public static Func<State, Text> Create(TextInlinedMap textInlinedMap)
		{
			// Create text
			var text = new Text(textInlinedMap.Value);
			// Return action
			return state => text;
		}
	}
}

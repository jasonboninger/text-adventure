using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionTextInlined
	{
		public static Func<State, Text> Create(TextInlinedMap textInlinedMap)
		{
			// Create replaceable
			var replaceable = new Replaceable(textInlinedMap.Value);
			// Return action
			return state =>
			{
				// Create value
				var value = replaceable.Replace(state);
				// Return text
				return new Text(value);
			};
		}
	}
}

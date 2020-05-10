using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionTextInlined
	{
		public static Func<State, Text> Create
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableList<IEntity> entitiesAmbiguous,
			TextInlinedMap textInlinedMap
		)
		{
			// Create replace action
			var actionReplace = ActionReplace.Create(replacer, entities, entitiesAmbiguous, textInlinedMap.Value);
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

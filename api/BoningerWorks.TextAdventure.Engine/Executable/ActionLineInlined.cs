using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionLineInlined
	{
		public static Func<State, Line> Create(Func<Id, Id> replacer, Entities entities, LineInlinedMap lineInlinedMap)
		{
			// Create text actions
			var actionsText = lineInlinedMap.TextMaps.Select(tm => ActionText.Create(replacer, entities, tm)).ToImmutableArray();
			// Return action
			return state =>
			{
				// Create texts
				var texts = ImmutableList.CreateBuilder<Text>();
				// Run through text actions
				for (int i = 0; i < actionsText.Length; i++)
				{
					// Add texts
					texts.AddRange(actionsText[i](state));
				}
				// Create line content
				var lineContent = new LineContent(texts.ToImmutable());
				// Create line
				var line = new Line(lineContent);
				// Return line
				return line;
			};
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
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
		public static Func<State, Line> Create
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableList<IEntity> entitiesAmbiguous,
			LineInlinedMap lineInlinedMap
		)
		{
			// Create text actions
			var actionsText = lineInlinedMap.TextMaps.Select(tm => ActionText.Create(replacer, entities, entitiesAmbiguous, tm));
			// Test text actions
			_ = actionsText.ToList();
			// Return action
			return state =>
			{
				// Create texts
				var texts = ImmutableList.CreateBuilder<Text>();
				// Run through text actions
				foreach (var actionText in actionsText)
				{
					// Add texts
					texts.AddRange(actionText(state));
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

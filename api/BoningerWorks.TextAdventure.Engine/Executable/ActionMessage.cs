using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionMessage
	{
		public static Action<ResultBuilder> Create
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableList<IEntity> entitiesAmbiguous,
			MessageMap messageMap
		)
		{
			// Set line actions
			var actionsLine = messageMap.LineMaps.Select(lm => ActionLine.Create(replacer, entities, entitiesAmbiguous, lm));
			// Test line actions
			_ = actionsLine.ToList();
			// Return action
			return result =>
			{
				// Get state
				var state = result.State;
				// Create lines
				var lines = ImmutableList.CreateBuilder<Line>();
				// Run through line actions
				foreach (var actionLine in actionsLine)
				{
					// Add lines
					lines.AddRange(actionLine(state));
				}
				// Create message
				var message = new Message(lines.ToImmutable());
				// Add message
				result.Messages.Add(message);
			};
		}
	}
}

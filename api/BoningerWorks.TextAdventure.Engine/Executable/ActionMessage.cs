﻿using BoningerWorks.TextAdventure.Core.Utilities;
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
		public static Action<ResultBuilder> Create(Func<Id, Id> replacer, Entities entities, MessageMap messageMap)
		{
			// Set line actions
			var actionsLine = messageMap.LineMaps.Select(lm => ActionLine.Create(replacer, entities, lm)).ToImmutableArray();
			// Return action
			return result =>
			{
				// Get state
				var state = result.State;
				// Create lines
				var lines = ImmutableList.CreateBuilder<Line>();
				// Run through line actions
				for (int i = 0; i < actionsLine.Length; i++)
				{
					// Add lines
					lines.AddRange(actionsLine[i](state));
				}
				// Create message
				var message = new Message(lines.ToImmutable());
				// Add message
				result.Messages.Add(message);
			};
		}
	}
}
﻿using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class Actions
	{
		public static Action<ResultBuilder> Create
		(
			Triggers? triggers,
			Entities entities,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			ImmutableArray<ActionMap> actionMaps,
			Func<Id, Id>? replacer = null
		)
		{
			// Set replacer
			replacer ??= id => id;
			// Create actions
			var actions = actionMaps
				.SelectMany(am => Action.Create(replacer, triggers, entities, commands, reactionPaths, reactionPath, am))
				.ToArray();
			// Get actions length
			var actionsLength = actions.Length;
			// Return action
			return r =>
			{
				// Run through actions
				for (int i = 0; i < actionsLength; i++)
				{
					// Execute action
					actions[i](r);
				}
			};
		}
	}
}
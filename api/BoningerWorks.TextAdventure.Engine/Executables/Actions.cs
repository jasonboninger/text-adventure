﻿using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
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
			Func<Symbol, Symbol>? replacer = null
		)
		{
			// Set replacer
			replacer ??= s => s;
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

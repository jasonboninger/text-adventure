using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionIterator
	{
		public static IEnumerable<Action<ResultBuilder>> Create
		(
			Func<Symbol, Symbol> replacer,
			Triggers? triggers,
			Entities entities,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			IteratorMap iteratorMap
		)
		{
			// Create replace
			Symbol replace;
			// Create iterable
			IEnumerable<IEntity> iterable;
			// Check if area
			if (iteratorMap.Area != null)
			{
				// Set replace
				replace = iteratorMap.Area;
				// Set iterable
				iterable = entities.Areas;
			}
			// Check if item
			else if (iteratorMap.Item != null)
			{
				// Set replace
				replace = iteratorMap.Item;
				// Set iterable
				iterable = entities.Items;
			}
			else
			{
				// Throw error
				throw new ArgumentException("Iterator entities could not be found.", nameof(iteratorMap));
			}
			// Return actions
			return iterable.SelectMany(i =>
			{
				// Get symbol
				var symbol = i.Symbol;
				// Create iterator replacer
				var replacerIterator = replacer;
				// Set iterator replacer
				replacerIterator = s => s == replace ? symbol : replacer(s);
				// Return actions
				return iteratorMap.ActionMaps
					.SelectMany(am => Action.Create(replacerIterator, triggers, entities, commands, reactionPaths, reactionPath, am));
			});
		}
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionIterator
	{
		public static ImmutableArray<TOutput> Create<TMap, TOutput>
		(
			Func<Id, Id> replacer,
			Entities entities,
			IteratorMap<TMap> iteratorMap,
			Func<Func<Id, Id>, TMap, IEnumerable<TOutput>> converter
		)
		{
			// Create replace
			Id replace;
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
			return iterable
				.SelectMany
					(e =>
					{
						// Get entity ID
						var idEntity = e.Id;
						// Create iterator replacer
						var replacerIterator = replacer;
						// Set iterator replacer
						replacerIterator = id => id == replace ? idEntity : replacer(id);
						// Return outputs
						return iteratorMap.ProcessorMaps.SelectMany(pm => converter(replacerIterator, pm));
					})
				.ToImmutableArray();
		}
	}
}

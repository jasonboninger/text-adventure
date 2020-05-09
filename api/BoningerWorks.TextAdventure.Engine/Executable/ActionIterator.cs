using BoningerWorks.TextAdventure.Core.Extensions;
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
		public static Func<IEnumerable<TOutput>> Create<TMap, TOutput>
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableArray<IteratorMap<TMap>> iteratorMaps,
			Func<Func<Id, Id>, TMap, TOutput> converter
		)
		{
			// Return action
			return Create(replacer, entities, iteratorMaps, (r, m) => converter(r, m).ToEnumerable());
		}
		public static Func<IEnumerable<TOutput>> Create<TMap, TOutput>
		(
			Func<Id, Id> replacer,
			Entities entities,
			ImmutableArray<IteratorMap<TMap>> iteratorMaps,
			Func<Func<Id, Id>, TMap, IEnumerable<TOutput>> converter
		)
		{
			// Create processor actions
			var actionsProcessor = iteratorMaps.SelectMany(im =>
			{
				// Create replace
				Id replace;
				// Create iterable entities
				IEnumerable<IEntity> entitiesIterable;
				// Check if area
				if (im.Area != null)
				{
					// Set replace
					replace = im.Area;
					// Set iterable entities
					entitiesIterable = entities.Areas;
				}
				// Check if item
				else if (im.Item != null)
				{
					// Set replace
					replace = im.Item;
					// Set iterable entities
					entitiesIterable = entities.Items;
				}
				else
				{
					// Throw error
					throw new ArgumentException("Iterator entities could not be found.", nameof(im));
				}
				// Return processor actions
				return entitiesIterable.SelectMany(e =>
				{
					// Get entity ID
					var idEntity = e.Id;
					// Create iterator replacer
					var replacerIterator = replacer;
					// Set iterator replacer
					replacerIterator = id => id == replace ? idEntity : replacer(id);
					// Return processor actions
					return im.ProcessorMaps.SelectMany(pm => converter(replacerIterator, pm));
				});
			});
			// Test processor actions
			_ = actionsProcessor.ToList();
			// Return action
			return () => actionsProcessor;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class IteratorMap<TMap>
	{
		public static IteratorMap<TMap> Create<TValue>(Iterator<TValue>? iterator, Func<TValue, TMap> converter)
		{
			// Check if iterator does not exist
			if (iterator == null)
			{
				// Throw error
				throw new ValidationError("Iterator cannot be null.");
			}
			// Try to create iterator
			try
			{
				// Create area
				Id? area = null;
				// Create item
				Id? item = null;
				// Check if area exists
				if (iterator.Area != null)
				{
					// Check if item exists
					if (iterator.Item != null)
					{
						// Throw error
						throw new ValidationError("When area exists, item must be null.");
					}
					// Set area
					area = Id.TryCreate(iterator.Area) ?? throw new ValidationError($"Area ({iterator.Area}) is not valid.");
					// Set item
					item = null;
				}
				// Check if item exists
				if (iterator.Item != null)
				{
					// Check if area exists
					if (iterator.Area != null)
					{
						// Throw error
						throw new ValidationError("When item exists, area must be null.");
					}
					// Set area
					area = null;
					// Set item
					item = Id.TryCreate(iterator.Item) ?? throw new ValidationError($"Item ({iterator.Item}) is not valid.");
				}
				// Check if area and item do not exist
				if (area == null && item == null)
				{
					// Throw error
					throw new ValidationError("Area and item cannot both be null.");
				}
				// Check if processor does not exist
				if (iterator.Processor == null || iterator.Processor.Count == 0)
				{
					// Throw error
					throw new ValidationError("Processor cannot be null or empty.");
				}
				// Create processor maps
				var processorMaps = iterator.Processor.Select(converter).ToImmutableArray();
				// Return iterator map
				return new IteratorMap<TMap>(area, item, processorMaps);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Iterator is not valid.").ToGenericException(exception);
			}
		}
		
		public Id? Area { get; }
		public Id? Item { get; }
		public ImmutableArray<TMap> ProcessorMaps { get; }

		private IteratorMap(Id? area, Id? item, ImmutableArray<TMap> processorMaps)
		{
			// Set area
			Area = area;
			// Set item
			Item = item;
			// Set processor maps
			ProcessorMaps = processorMaps;
		}
	}
}

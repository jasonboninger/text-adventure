using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class IteratorMap
	{
		public Symbol? Area { get; }
		public Symbol? Item { get; }
		public ImmutableArray<ActionMap> ActionMaps { get; }

		public IteratorMap(Iterator? iterator)
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
					Area = Symbol.TryCreate(iterator.Area) ?? throw new ValidationError($"Area ({iterator.Area}) is not valid.");
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
					// Set item
					Item = Symbol.TryCreate(iterator.Item) ?? throw new ValidationError($"Item ({iterator.Item}) is not valid.");
				}
				// Check if actions does not exist
				if (iterator.Actions == null || iterator.Actions.Count == 0)
				{
					// Throw error
					throw new ValidationError("Actions cannot be null or empty.");
				}
				// Set action maps
				ActionMaps = iterator.Actions.Select(a => new ActionMap(a)).ToImmutableArray();
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Iterator is not valid.").ToGenericException(exception);
			}
		}
	}
}

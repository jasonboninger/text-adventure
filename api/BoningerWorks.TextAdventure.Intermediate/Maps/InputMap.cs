using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class InputMap
	{
		public ImmutableDictionary<Id, Id> InputIdToEntityIdMappings { get; }

		public InputMap(List<Input?>? inputs)
		{
			// Set input ID to entity ID mappings
			InputIdToEntityIdMappings = inputs?
				.Select(i => i ?? throw new ValidationError("Input cannot be null."))
				.GroupBy
					(
						i =>
						{
							// Check if key does not exist
							if (i.Key == null)
							{
								// Throw error
								throw new ValidationError("Input key cannot be null.");
							}
							// Return input ID
							return Id.TryCreate(i.Key) ?? throw new ValidationError($"Input key ({i.Key}) is not valid.");
						},
						(s, @is) =>
						{
							// Check if more than one value
							if (@is.Count() > 1)
							{
								// Throw error
								throw new ValidationError($"Duplicate inputs for key ({s}) were detected.");
							}
							// Get value
							var value = @is.Select(ri => ri.Value).Single();
							// Get entity ID
							var entityId = Id.TryCreate(value) ?? throw new ValidationError($"Input value ({value}) is not valid.");
							// Return input ID to entity ID
							return KeyValuePair.Create(s, entityId);
						}
					)
				.ToImmutableDictionary()
				?? ImmutableDictionary<Id, Id>.Empty;
		}
	}
}

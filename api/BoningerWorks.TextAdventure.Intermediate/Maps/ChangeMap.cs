using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ChangeMap
	{
		public Path Path { get; }
		public string Value { get; }

		internal ChangeMap(Change? change)
		{
			// Check if change does not exist
			if (change == null)
			{
				// Throw error
				throw new ValidationError("Change cannot be null.");
			}
			// Try to create change
			try
			{
				// Check if target does not exist
				if (string.IsNullOrWhiteSpace(change.Target))
				{
					// Throw error
					throw new ValidationError("Target cannot be null, empty or whitespace.");
				}
				// Check if datum does not exist
				if (string.IsNullOrWhiteSpace(change.Datum))
				{
					// Throw error
					throw new ValidationError("Datum cannot be null, empty or whitespace.");
				}
				// Set path
				Path = Path.TryCreate(change.Target, change.Datum, metadata: false)
					?? throw new ValidationError($"Path with target ({change.Target}) and datum ({change.Datum}) is not valid.");
				// Set value
				Value = change.Value ?? throw new ValidationError("Value cannot be null.");
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Change is not valid.").ToGenericException(exception);
			}
		}
	}
}

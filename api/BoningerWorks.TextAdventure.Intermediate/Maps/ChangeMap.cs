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
				// Set value
				Value = change.Value ?? throw new ValidationError("Value cannot be null.");
				// Check if target does not exist
				if (change.Target == null)
				{
					// Throw error
					throw new ValidationError("Target cannot be null.");
				}
				// Check if standard exists
				if (change.Standard != null)
				{
					// Check if custom exists
					if (change.Custom != null)
					{
						// Throw error
						throw new ValidationError("When standard exists, custom must be null.");
					}
					// Set path
					Path = Path.TryCreate(change.Target, change.Standard, custom: false)
						?? throw new ValidationError($"Path with target ({change.Target}) and standard ({change.Standard}) is not valid.");
					// Return
					return;
				}
				// Check if custom exists
				if (change.Custom != null)
				{
					// Check if standard exists
					if (change.Standard != null)
					{
						// Throw error
						throw new ValidationError("When custom exists, standard must be null.");
					}
					// Set path
					Path = Path.TryCreate(change.Target, change.Custom, custom: true)
						?? throw new ValidationError($"Path with target ({change.Target}) and custom ({change.Custom}) is not valid.");
					// Return
					return;
				}
				// Throw error
				throw new ValidationError("Standard or custom must be provided.");
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Change is not valid.").ToGenericException(exception);
			}
		}
	}
}

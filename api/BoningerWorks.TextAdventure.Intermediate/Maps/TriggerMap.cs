using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class TriggerMap
	{
		public Id CommandId { get; }
		public InputMap InputMap { get; }

		internal TriggerMap(Trigger? trigger)
		{
			// Check if trigger does not exist
			if (trigger == null)
			{
				// Throw error
				throw new ValidationError("Trigger cannot be null.");
			}
			// Try to create reaction
			try
			{
				// Set command ID
				CommandId = Id.TryCreate(trigger.CommandId)
					?? throw new ValidationError($"Command ID ({trigger.CommandId}) is not valid.");
				// Set input map
				InputMap = new InputMap(trigger.Inputs);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Get command ID
				var commandId = CommandId == null ? string.Empty : $" ({CommandId})";
				// Throw error
				throw new ValidationError($"Trigger{commandId} is not valid.").ToGenericException(exception);
			}
		}
	}
}

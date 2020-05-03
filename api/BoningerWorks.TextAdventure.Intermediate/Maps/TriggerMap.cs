using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class TriggerMap
	{
		public Symbol CommandSymbol { get; }
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
				// Set command symbol
				CommandSymbol = Symbol.TryCreate(trigger.CommandSymbol)
					?? throw new ValidationError($"Command symbol ({trigger.CommandSymbol}) is not valid.");
				// Set input map
				InputMap = new InputMap(trigger.Inputs);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Get command symbol
				var commandSymbol = CommandSymbol == null ? string.Empty : $" ({CommandSymbol})";
				// Throw error
				throw new ValidationError($"Trigger{commandSymbol} is not valid.").ToGenericException(exception);
			}
		}
	}
}

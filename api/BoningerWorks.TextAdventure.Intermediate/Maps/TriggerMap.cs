using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class TriggerMap
	{
		public Symbol CommandSymbol { get; }
		public ImmutableDictionary<Symbol, Symbol> CommandItemSymbolToItemSymbolMappings { get; }

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
				// Set command item symbol to item symbol mappings
				CommandItemSymbolToItemSymbolMappings = trigger.CommandItemSymbolToItemSymbolMappings?
					.ToImmutableDictionary
						(
							kv => Symbol.TryCreate(kv.Key) ?? throw new ValidationError($"Command item symbol ({kv.Key}) is not valid."),
							kv => Symbol.TryCreate(kv.Value) ?? throw new ValidationError($"Item symbol ({kv.Value}) is not valid.")
						)
					?? ImmutableDictionary<Symbol, Symbol>.Empty;
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

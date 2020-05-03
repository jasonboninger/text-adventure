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
		public ImmutableDictionary<Symbol, Symbol> InputSymbolToEntitySymbolMappings { get; }

		public InputMap(List<Input?>? inputs)
		{
			// Set input symbol to entity symbol mappings
			InputSymbolToEntitySymbolMappings = inputs?
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
							// Return input symbol
							return Symbol.TryCreate(i.Key) ?? throw new ValidationError($"Input key ({i.Key}) is not valid.");
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
							// Get entity symbol
							var entitySymbol = Symbol.TryCreate(value) ?? throw new ValidationError($"Input value ({value}) is not valid.");
							// Return input symbol to entity symbol
							return KeyValuePair.Create(s, entitySymbol);
						}
					)
				.ToImmutableDictionary()
				?? ImmutableDictionary<Symbol, Symbol>.Empty;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ReactionMap
	{
		internal static ImmutableArray<ReactionMap> Create(Symbol entitySymbol, OneOrManyList<Reaction?>? reactions)
		{
			// Check if reactions does not exist
			if (reactions == null)
			{
				// Return no reaction maps
				return ImmutableArray<ReactionMap>.Empty;
			}
			// Create reaction maps
			var reactionMaps = reactions.Select(r => new ReactionMap(entitySymbol, r)).ToImmutableArray();
			// Return reaction maps
			return reactionMaps;
		}
		
		public Symbol EntitySymbol { get; }
		public Symbol CommandSymbol { get; }
		public ImmutableDictionary<Symbol, Symbol> InputSymbolToEntitySymbolMappings { get; }
		public ImmutableArray<ActionMap> ActionMaps { get; }

		private ReactionMap(Symbol entitySymbol, Reaction? reaction)
		{
			// Set entity symbol
			EntitySymbol = entitySymbol;
			// Check if reaction does not exist
			if (reaction == null)
			{
				// Throw error
				throw new ValidationError("Reaction cannot be null.");
			}
			// Try to create reaction
			try
			{
				// Set command symbol
				CommandSymbol = Symbol.TryCreate(reaction.CommandSymbol)
					?? throw new ValidationError($"Command symbol ({reaction.CommandSymbol}) is not valid.");
				// Set input symbol to entity symbol mappings
				InputSymbolToEntitySymbolMappings = reaction.ReactionInputs?
					.Select(ri => ri ?? throw new ValidationError("Input cannot be null."))
					.GroupBy
						(
							ri =>
							{
								// Check if input does not exist
								if (ri.Input == null)
								{
									// Throw error
									throw new ValidationError("Input cannot be null.");
								}
								// Return input symbol
								return Symbol.TryCreate(ri.Input) ?? throw new ValidationError($"Input ({ri.Input}) is not valid.");
							},
							(@is, ris) =>
							{
								// Check if more than one value
								if (ris.Count() > 1)
								{
									// Throw error
									throw new ValidationError($"Duplicate values for input ({@is}) were detected.");
								}
								// Get value
								var value = ris.Select(ri => ri.Value).Single();
								// Get entity symbol
								var entitySymbol = Symbol.TryCreate(value) ?? throw new ValidationError($"Value ({value}) is not valid.");
								// Return input symbol to entity symbol
								return KeyValuePair.Create(@is, entitySymbol);
							}
						)
					.ToImmutableDictionary()
					?? ImmutableDictionary<Symbol, Symbol>.Empty;
				// Check if actions does not exist
				if (reaction.Actions == null || reaction.Actions.Count == 0)
				{
					// Throw error
					throw new ValidationError("Actions cannot be null or empty.");
				}
				// Set action maps
				ActionMaps = reaction.Actions.Select(a => new ActionMap(a)).ToImmutableArray();
			}
			catch (GenericException<ValidationError> exception)
			{
				// Get command symbol
				var commandSymbol = CommandSymbol == null ? string.Empty : $" ({CommandSymbol})";
				// Throw error
				throw new ValidationError($"Reaction{commandSymbol} is not valid.").ToGenericException(exception);
			}
		}
	}
}

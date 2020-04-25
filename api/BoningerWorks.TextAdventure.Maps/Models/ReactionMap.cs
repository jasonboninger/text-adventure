using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class ReactionMap
	{
		public static ImmutableArray<ReactionMap> Create(OneOrManyList<Reaction?>? reactions, Symbol? itemSymbolDefault)
		{
			// Check if reactions does not exist
			if (reactions == null)
			{
				// Return no reaction maps
				return ImmutableArray<ReactionMap>.Empty;
			}
			// Create reaction maps
			var reactionMaps = reactions.Select(r => new ReactionMap(r, itemSymbolDefault)).ToImmutableArray();
			// Return reaction maps
			return reactionMaps;
		}
		
		public Symbol CommandSymbol { get; }
		public ImmutableDictionary<Symbol, Symbol> CommandItemSymbolToItemSymbolMappings { get; }
		public Symbol? ItemSymbolDefault { get; }
		public ImmutableArray<ActionMap> ActionMaps { get; }

		public ReactionMap(Reaction? reaction, Symbol? itemSymbolDefault)
		{
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
				// Set command item symbol to item symbol mappings
				CommandItemSymbolToItemSymbolMappings = reaction.CommandItemSymbolToItemSymbolMappings?
					.ToImmutableDictionary
						(
							kv => Symbol.TryCreate(kv.Key) ?? throw new ValidationError($"Command item symbol ({kv.Key}) is not valid."),
							kv => Symbol.TryCreate(kv.Value) ?? throw new ValidationError($"Item symbol ({kv.Value}) is not valid.")
						)
					?? ImmutableDictionary<Symbol, Symbol>.Empty;
				// Set default item symbol
				ItemSymbolDefault = itemSymbolDefault;
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

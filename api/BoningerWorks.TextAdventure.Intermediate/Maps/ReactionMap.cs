using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Json.Utilities;
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
		public InputMap InputMap { get; }
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
				// Set input map
				InputMap = new InputMap(reaction.Inputs);
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

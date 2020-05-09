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
		internal static ImmutableArray<ReactionMap> Create(Id entityId, OneOrManyList<Reaction?>? reactions)
		{
			// Check if reactions does not exist
			if (reactions == null)
			{
				// Return no reaction maps
				return ImmutableArray<ReactionMap>.Empty;
			}
			// Create reaction maps
			var reactionMaps = reactions.Select(r => new ReactionMap(entityId, r)).ToImmutableArray();
			// Return reaction maps
			return reactionMaps;
		}
		
		public Id EntityId { get; }
		public Id CommandId { get; }
		public InputMap InputMap { get; }
		public ImmutableArray<ActionMap> ActionMaps { get; }

		private ReactionMap(Id entityId, Reaction? reaction)
		{
			// Set entity ID
			EntityId = entityId;
			// Check if reaction does not exist
			if (reaction == null)
			{
				// Throw error
				throw new ValidationError("Reaction cannot be null.");
			}
			// Try to create reaction
			try
			{
				// Set command ID
				CommandId = Id.TryCreate(reaction.CommandId)
					?? throw new ValidationError($"Command ID ({reaction.CommandId}) is not valid.");
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
				// Get command ID
				var commandId = CommandId == null ? string.Empty : $" ({CommandId})";
				// Throw error
				throw new ValidationError($"Reaction{commandId} is not valid.").ToGenericException(exception);
			}
		}
	}
}

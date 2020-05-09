using BoningerWorks.TextAdventure.Engine.Enums;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionTrigger
	{
		public static Action<ResultBuilder> Create
		(
			Triggers? triggers,
			Commands commands,
			ImmutableArray<ReactionPath> reactionPaths,
			ReactionPath? reactionPath,
			TriggerMap triggerMap
		)
		{
			// Get command
			var command = commands.TryGet(triggerMap.CommandId);
			// Check if command does not exist
			if (command == null)
			{
				// Throw error
				throw new ValidationError($"No command with ID ({triggerMap.CommandId}) could be found.");
			}
			// Get reaction queries
			var reactionQueries = reactionPaths
				.Where
					(rp =>
					{
						// Check if command matches
						if (rp.Command == command)
						{
							// Get input ID to entity ID mappings
							var inputIdToEntityIdMappings = triggerMap.InputMap.InputIdToEntityIdMappings;
							// Check if parts count and inputs count do not match
							if (rp.Parts.Count != inputIdToEntityIdMappings.Count)
							{
								// Get inputs count
								var countInputs = inputIdToEntityIdMappings.Count;
								// Get parts count
								var countParts = rp.Parts.Count;
								// Create message
								var message = $"Trigger input count ({countInputs}) must match command ({command}) parts count ({countParts}).";
								// Throw error
								throw new ValidationError(message);
							}
							// Return if all parts match inputs
							return rp.Parts.All(p => inputIdToEntityIdMappings.TryGetValue(p.Input.Id, out var entityId) && entityId == p.Entity.Id);
						}
						// Return not match
						return false;
					})
				.Select
					(rp =>
					{
						// Check if triggers and reaction path do not either both exist or both not exist
						if (triggers == null != (reactionPath == null))
						{
							// Throw error
							throw new ArgumentException("Triggers and reaction path must either both be null or both be not null.");
						}
						// Check if triggers exists and reaction path exists
						if (triggers != null && reactionPath != null)
						{
							// Add trigger
							triggers.Add(reactionPath, rp);
						}
						// Create reaction query
						var reactionQuery = new ReactionQuery(command, rp.Parts.Select(p => p.Entity).ToImmutableList());
						// Return reaction query
						return reactionQuery;
					});
			// Test and count reaction queries
			var reactionQueriesCount = reactionQueries.ToList().Count;
			// Check if reaction queries does not exist
			if (reactionQueriesCount == 0)
			{
				// Throw error
				throw new ValidationError($"Trigger for command ({command}) does not match any reactions.");
			}
			// Return action
			return r =>
			{
				// Get game
				var game = r.Game;
				// Get state
				var state = r.State;
				// Run through reaction queries
				foreach (var reactionQuery in reactionQueries)
				{
					// Get reaction result
					var reactionResult = game.Reactions.GetResult(state, reactionQuery);
					// Check if success
					if (reactionResult.Outcome == EReactionOutcome.Success)
					{
						// Get reactions
						var reactions = reactionResult.Reactions;
						// Run through reactions
						for (int i = 0; i < reactions.Length; i++)
						{
							// Execute reaction
							reactions[i].Execute(r);
						}
					}
				}
			};
		}
	}
}

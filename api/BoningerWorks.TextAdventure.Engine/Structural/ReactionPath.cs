using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Structural
{
	public class ReactionPath
	{
		public Command Command { get; }
		public ImmutableList<ReactionPathPart> Parts { get; }

		public ReactionPath(Entities entities, Commands commands, ReactionMap reactionMap)
		{
			// Set command
			Command = commands.TryGet(reactionMap.CommandId)
				?? throw new InvalidOperationException($"No command with ID ({reactionMap.CommandId}) could be found.");
			// Try to create reaction path
			try
			{
				// Check if reaction map has no input ID to entity ID mappings
				if (reactionMap.InputMap.InputIdToEntityIdMappings.Count == 0)
				{
					// Check if command has more than one input
					if (Command.Inputs.Length > 1)
					{
						// Throw error
						throw new ValidationError($"Some inputs are missing.");
					}
				}
				else
				{
					// Check if reaction map does not have an input ID for each command input
					if (Command.Inputs.Any(i => !reactionMap.InputMap.InputIdToEntityIdMappings.ContainsKey(i.Id)))
					{
						// Throw error
						throw new ValidationError($"Some inputs are missing.");
					}
				}
				// Set parts
				Parts = Command.Inputs
					.Select
						(i =>
						{
							// Try to get entity ID for input ID
							if (!reactionMap.InputMap.InputIdToEntityIdMappings.TryGetValue(i.Id, out var entityId))
							{
								// Set entity ID
								entityId = reactionMap.EntityId;
							}
							// Try to get entity
							var entity = entities.TryGet(entityId);
							// Check if entity does not exist
							if (entity == null)
							{
								// Throw error
								throw new ValidationError($"No entity with ID ({reactionMap.EntityId}) could be found.");
							}
							// Check if entity is not valid
							if (!i.IsValid(entity))
							{
								// Throw error
								throw new ValidationError($"Entity ({entity}) is not compatible with input ({i.Id}).");
							}
							// Create part
							var part = new ReactionPathPart(i, entity);
							// Return part
							return part;
						})
					.ToImmutableList();
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Reaction path for command ({Command}) is not valid.").ToGenericException(exception);
			}
		}

		public override string ToString()
		{
			// Return string
			return $"{Command}[{string.Join(", ", Parts.Select(p => $"{p.Input.Id} = {p.Entity}"))}]";
		}
	}
}

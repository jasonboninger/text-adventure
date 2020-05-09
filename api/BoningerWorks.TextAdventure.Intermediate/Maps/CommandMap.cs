using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class CommandMap
	{
		public Id CommandId { get; }
		public ImmutableArray<CommandPartMap> CommandPartMaps { get; }
		public ImmutableArray<ActionMap> ActionMapsFail { get; }

		internal CommandMap(Command? command)
		{
			// Check if command does not exist
			if (command == null)
			{
				// Throw error
				throw new ValidationError("Command cannot be null.");
			}
			// Set command ID
			CommandId = Id.TryCreate(command.Id) ?? throw new ValidationError($"Command ID ({command.Id}) is not valid.");
			// Try to create command map
			try
			{
				// Check if command parts does not exist
				if (command.CommandParts == null || command.CommandParts.Count == 0)
				{
					// Throw error
					throw new ValidationError("Command parts cannot be null or empty.");
				}
				// Set command part maps
				CommandPartMaps = command.CommandParts.Select(cp => new CommandPartMap(cp)).ToImmutableArray();
				// Get duplicate command part IDs
				var commandPartIdDuplicates = CommandPartMaps
					.Where(cpm => cpm.Words == null)
					.GroupBy(cpm => cpm.Area ?? cpm.Item ?? throw new InvalidOperationException("No ID found."))
					.Where(g => g.Count() > 1)
					.Select(g => g.Key)
					.ToList();
				// Check if duplicate commard part IDs exist
				if (commandPartIdDuplicates.Count != 0)
				{
					// Throw error
					throw new ValidationError("Not all command part IDs are unique.");
				}
				// Set fail action maps
				ActionMapsFail = command.ActionsFail?.Select(a => new ActionMap(a)).ToImmutableArray() ?? ImmutableArray<ActionMap>.Empty;
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Command ({CommandId}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

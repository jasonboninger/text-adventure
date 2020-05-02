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
		public Symbol CommandSymbol { get; }
		public ImmutableArray<CommandPartMap> CommandPartMaps { get; }

		internal CommandMap(Command? command)
		{
			// Check if command does not exist
			if (command == null)
			{
				// Throw error
				throw new ValidationError("Command cannot be null.");
			}
			// Set command symbol
			CommandSymbol = Symbol.TryCreate(command.Id) ?? throw new ValidationError($"Command symbol ({command.Id}) is not valid.");
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
				// Get duplicate command part symbols
				var commandPartSymbolDuplicates = CommandPartMaps
					.Where(cpm => cpm.Words == null)
					.GroupBy(cpm => cpm.Area ?? cpm.Item ?? throw new InvalidOperationException("No symbol found."))
					.Where(g => g.Count() > 1)
					.Select(g => g.Key)
					.ToList();
				// Check if duplicate commard part symbols exist
				if (commandPartSymbolDuplicates.Count != 0)
				{
					// Throw error
					throw new ValidationError("Not all command part symbols are unique.");
				}
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Command ({CommandSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

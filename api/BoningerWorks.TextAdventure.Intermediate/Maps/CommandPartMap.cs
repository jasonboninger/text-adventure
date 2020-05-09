using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class CommandPartMap
	{
		public Names? Words { get; set; }
		public Id? Area { get; set; }
		public Id? Item { get; set; }

		public CommandPartMap(CommandPart? commandPart)
		{
			// Check if command part does not exist
			if (commandPart == null)
			{
				// Throw error
				throw new ValidationError("Command part cannot be null.");
			}
			// Try to create command part
			try
			{
				// Check if words exists
				if (commandPart.Words != null)
				{
					// Check if area or item exists
					if (commandPart.Area != null || commandPart.Item != null)
					{
						// Throw error
						throw new ValidationError("When words is not null, area and item must be null.");
					}
					// Set words
					Words = Names.TryCreate(commandPart.Words.Select(w => Name.TryCreate(w) ?? throw new ValidationError($"Word ({w}) is not valid.")))
						?? throw new ValidationError("Words is not valid.");
					// Return
					return;
				}
				// Check if area exists
				if (commandPart.Area != null)
				{
					// Check if words or item exists
					if (commandPart.Words != null || commandPart.Item != null)
					{
						// Throw error
						throw new ValidationError("When area is not null, words and item must be null.");
					}
					// Set area
					Area = Id.TryCreate(commandPart.Area) ?? throw new ValidationError($"Command area ID ({commandPart.Area}) is not valid.");
					// Return
					return;
				}
				// Check if item exists
				if (commandPart.Item != null)
				{
					// Check if words or area exists
					if (commandPart.Words != null || commandPart.Area != null)
					{
						// Throw error
						throw new ValidationError("When item is not null, words and area must be null.");
					}
					// Set item
					Item = Id.TryCreate(commandPart.Item) ?? throw new ValidationError($"Command item ID ({commandPart.Item}) is not valid.");
					// Return
					return;
				}
				// Throw error
				throw new ValidationError("Either words, area, or item is required, but nothing was provided.");
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Command part is not valid.").ToGenericException(exception);
			}
		}
	}
}

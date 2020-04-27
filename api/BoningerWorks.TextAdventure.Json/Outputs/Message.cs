using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.Outputs.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Message
	{
		public ImmutableList<Line> Lines { get; set; }

		public Message(ImmutableList<Line> lines)
		{
			// Check if lines does not exist
			if (lines == null || lines.Count == 0)
			{
				// Throw error
				throw GenericException.Create(new InvalidDataError("Lines cannot be null or empty."));
			}
			// Set lines
			Lines = lines;
		}
	}
}

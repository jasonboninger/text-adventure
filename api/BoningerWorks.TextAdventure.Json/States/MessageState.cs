using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class MessageState
	{
		public ImmutableList<LineState> Lines { get; set; }

		public MessageState(ImmutableList<LineState> lines)
		{
			// Check if lines does not exist
			if (lines == null || lines.Count == 0)
			{
				// Throw error
				throw GenericException.Create(new StateInvalidError("Lines cannot be null or empty."));
			}
			// Set lines
			Lines = lines;
		}
	}
}

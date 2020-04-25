using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class MessageMap
	{
		public ImmutableArray<LineMap> LineMaps { get; }

		internal MessageMap(Message? message)
		{
			// Check if message does not exist
			if (message == null)
			{
				// Throw error
				throw new ValidationError("Message cannot be null.");
			}
			// Try to create message
			try
			{
				// Check if lines does not exist
				if (message.Lines == null || message.Lines.Count == 0)
				{
					// Throw error
					throw new ArgumentException("Lines cannot be empty.");
				}
				// Set line maps
				LineMaps = message.Lines.Select(l => new LineMap(l)).ToImmutableArray();
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError("Message is not valid.").ToGenericException(exception);
			}
		}
	}
}

using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class MessageState
	{
		public static MessageState Create(List<LineState> lines)
		{
			// Create message state
			var messageState = new MessageState
			{
				Lines = lines == null || lines.Count == 0 ? throw new ArgumentException("Lines cannot be null or empty.", nameof(lines)) : lines
			};
			// Return message state
			return messageState;
		}

		public List<LineState>? Lines { get; set; }
	}
}

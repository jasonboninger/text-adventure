using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Message
	{
		public ImmutableList<Line> Lines { get; set; }

		public Message(string text)
		{
			// Check if text does not exist
			if (string.IsNullOrWhiteSpace(text))
			{
				// Throw error
				throw new ArgumentException("Text cannot be null, empty, or whitespace.", nameof(text));
			}
			// Set lines
			Lines = ImmutableList.Create(new Line(new LineContent(ImmutableList.Create(new Text(text)))));
		}
		public Message(ImmutableList<Line> lines)
		{
			// Check if lines does not exist
			if (lines == null || lines.Count == 0)
			{
				// Throw error
				throw new ArgumentException("Lines cannot be null or empty.", nameof(lines));
			}
			// Set lines
			Lines = lines;
		}
	}
}

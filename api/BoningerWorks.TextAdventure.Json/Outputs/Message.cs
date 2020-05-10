using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Message
	{
		public ImmutableList<Line> Lines { get; set; }

		public Message(string text)
		{
			// Set lines
			Lines = ImmutableList.Create(new Line(new LineContent(ImmutableList.Create(new Text(text)))));
		}
		public Message(ImmutableList<Line> lines)
		{
			// Set lines
			Lines = lines ?? throw new ArgumentException("Lines cannot be null.", nameof(lines));
		}
	}
}

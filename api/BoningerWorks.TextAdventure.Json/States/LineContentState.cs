using System;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineContentState
	{
		public static LineContentState Create(string text)
		{
			// Create content line state
			var lineContentState = new LineContentState
			{
				Text = string.IsNullOrWhiteSpace(text) ? throw new ArgumentException("Text cannot be null or whitespace.", nameof(text)) : text
			};
			// Return content line state
			return lineContentState;
		}

		public string? Text { get; set; }
	}
}

using System;

namespace BoningerWorks.TextAdventure.Engine.Maps
{
	public class LineInlinedMap
	{
		public string Text { get; }

		public LineInlinedMap(string text)
		{
			// Check if text does not exist
			if (string.IsNullOrWhiteSpace(text))
			{
				// Throw error
				throw new ArgumentException("Text cannot be null, empty, or whitespace.");
			}
			// Set text
			Text = text;
		}
	}
}

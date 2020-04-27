using System;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Line
	{
		public LineContent? Content { get; }
		public LineSpecial? Special { get; }

		public Line(LineContent lineContent)
		{
			// Set content
			Content = lineContent ?? throw new ArgumentException("Line content cannot be null.", nameof(lineContent));
		}
		public Line(LineSpecial lineSpecial)
		{
			// Set special
			Special = lineSpecial ?? throw new ArgumentException("Special line cannot be null.", nameof(lineSpecial));
		}
	}
}

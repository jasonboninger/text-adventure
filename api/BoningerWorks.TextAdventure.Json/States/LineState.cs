using System;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineState
	{
		public static LineState CreateContent(LineContentState content)
		{
			// Create content state
			var contentState = new LineState
			{
				Content = content ?? throw new ArgumentException("Content cannot be null.", nameof(content))
			};
			// Return content state
			return contentState;
		}

		public static LineState CreateSpecial(LineSpecialState special)
		{
			// Create special state
			var specialState = new LineState
			{
				Special = special ?? throw new ArgumentException("Special cannot be null.", nameof(special))
			};
			// Return special state
			return specialState;
		}

		public LineContentState Content { get; set; }
		public LineSpecialState Special { get; set; }
	}
}

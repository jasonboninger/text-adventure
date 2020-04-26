using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineContentState
	{
		public string Text { get; }

		public LineContentState(string text)
		{
			// Set text
			Text = string.IsNullOrWhiteSpace(text) 
				? throw GenericException.Create(new StateInvalidError("Text cannot be null or whitespace.")) 
				: text;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineState
	{
		public LineContentState? Content { get; }
		public LineSpecialState? Special { get; }

		public LineState(LineContentState lineContentState)
		{
			// Set content state
			Content = lineContentState ?? throw GenericException.Create(new StateInvalidError("Line content cannot be null."));
		}
		public LineState(LineSpecialState lineSpecialState)
		{
			// Set special state
			Special = lineSpecialState ?? throw GenericException.Create(new StateInvalidError("Special line cannot be null."));
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineState
	{
		public LineContentState? ContentState { get; set; }
		public LineSpecialState? SpecialState { get; set; }

		public LineState(LineContentState lineContentState)
		{
			// Set content state
			ContentState = lineContentState ?? throw GenericException.Create(new StateInvalidError("Line content cannot be null."));
		}
		public LineState(LineSpecialState lineSpecialState)
		{
			// Set special state
			SpecialState = lineSpecialState ?? throw GenericException.Create(new StateInvalidError("Special line cannot be null."));
		}
	}
}

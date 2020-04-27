using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.Outputs.Errors;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Line
	{
		public LineContent? Content { get; }
		public LineSpecial? Special { get; }

		public Line(LineContent lineContentState)
		{
			// Set content state
			Content = lineContentState ?? throw GenericException.Create(new InvalidDataError("Line content cannot be null."));
		}
		public Line(LineSpecial lineSpecialState)
		{
			// Set special state
			Special = lineSpecialState ?? throw GenericException.Create(new InvalidDataError("Special line cannot be null."));
		}
	}
}

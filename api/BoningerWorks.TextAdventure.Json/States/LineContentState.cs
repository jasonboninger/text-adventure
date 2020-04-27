using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineContentState
	{
		public ImmutableList<TextState> Texts { get; }

		public LineContentState(ImmutableList<TextState> textStates)
		{
			// Set texts
			Texts = textStates ?? throw GenericException.Create(new StateInvalidError("Texts cannot be null."));
		}
	}
}

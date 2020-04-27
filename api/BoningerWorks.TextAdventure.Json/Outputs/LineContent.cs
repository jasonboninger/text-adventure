using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.Outputs.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class LineContent
	{
		public ImmutableList<Text> Texts { get; }

		public LineContent(ImmutableList<Text> textStates)
		{
			// Set texts
			Texts = textStates ?? throw GenericException.Create(new InvalidDataError("Texts cannot be null."));
		}
	}
}

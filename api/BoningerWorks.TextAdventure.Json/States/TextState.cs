using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.States.Errors;

namespace BoningerWorks.TextAdventure.Json.States
{
	public class TextState
	{
		public string Value { get; }

		public TextState(string value)
		{
			// Set value
			Value = string.IsNullOrWhiteSpace(value)
				? throw GenericException.Create(new StateInvalidError("Text value cannot be null or whitespace."))
				: value;
		}
	}
}

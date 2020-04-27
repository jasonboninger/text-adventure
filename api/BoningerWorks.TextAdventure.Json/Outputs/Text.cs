using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Json.Outputs.Errors;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Text
	{
		public string Value { get; }

		public Text(string value)
		{
			// Set value
			Value = string.IsNullOrWhiteSpace(value)
				? throw GenericException.Create(new InvalidDataError("Text value cannot be null or whitespace."))
				: value;
		}
	}
}

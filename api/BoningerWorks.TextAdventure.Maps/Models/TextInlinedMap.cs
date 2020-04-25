using BoningerWorks.TextAdventure.Maps.Errors;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class TextInlinedMap
	{
		public string Value { get; }

		public TextInlinedMap(string? value)
		{
			// Check if value does not exist
			if (string.IsNullOrWhiteSpace(value))
			{
				// Throw error
				throw new ValidationError("Text value cannot be null, empty, or whitespace.");
			}
			// Set value
			Value = value;
		}
	}
}

using BoningerWorks.TextAdventure.Intermediate.Errors;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class TextInlinedMap
	{
		public string Value { get; }

		internal TextInlinedMap(string? value)
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

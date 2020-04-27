using System;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Text
	{
		public string Value { get; }

		public Text(string value)
		{
			// Set value
			Value = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("Value cannot be null or whitespace.", nameof(value)) : value;
		}
	}
}

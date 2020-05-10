using System;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Text
	{
		public string Value { get; }

		public Text(string value)
		{
			// Set value
			Value = value ?? throw new ArgumentException("Value cannot be null.", nameof(value));
		}
	}
}

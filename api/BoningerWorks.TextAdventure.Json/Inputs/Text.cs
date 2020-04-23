using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Text
	{
		public static Text CreateFromString(string @string)
		{
			// Return text
			return new Text { Value = @string };
		}

		[JsonPropertyName("if")] public If<SFlexibleObject<Text>>? If { get; set; }
		[JsonPropertyName("value")] public string? Value { get; set; }
	}
}

using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Text
	{
		public static Text CreateFromString(string @string)
		{
			// Return text
			return new Text { Value = @string };
		}

		[JsonPropertyName("if")] public If<Text> If { get; set; }
		[JsonPropertyName("value")] public string Value { get; set; }
	}
}

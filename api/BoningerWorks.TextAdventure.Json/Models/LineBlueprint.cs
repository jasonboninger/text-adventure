using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class LineBlueprint
	{
		public static LineBlueprint CreateFromString(string @string)
		{
			// Return line blueprint
			return new LineBlueprint { Text = @string };
		}

		[JsonPropertyName("text")] public string Text { get; set; }
		[JsonPropertyName("special")] public string Special { get; set; }
	}
}

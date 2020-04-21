using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Line
	{
		public static Line CreateFromString(string @string)
		{
			// Return line
			return new Line { Texts = new OneOrManyList<Text> { Text.CreateFromString(@string) } };
		}

		[JsonPropertyName("texts")] public OneOrManyList<Text> Texts { get; set; }
		[JsonPropertyName("special")] public string Special { get; set; }
	}
}

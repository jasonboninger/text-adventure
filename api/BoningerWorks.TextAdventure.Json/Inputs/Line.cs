using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Line
	{
		public static Line CreateFromString(string @string)
		{
			// Return line
			return new Line { Texts = new OneOrManyList<FlexibleObject<Text>> { Text.CreateFromString(@string) } };
		}

		[JsonPropertyName("if")] public If<FlexibleObject<Line>> If { get; set; }
		[JsonPropertyName("special")] public string Special { get; set; }
		[JsonPropertyName("texts")] public OneOrManyList<FlexibleObject<Text>> Texts { get; set; }
	}
}

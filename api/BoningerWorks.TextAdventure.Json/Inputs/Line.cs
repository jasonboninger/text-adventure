using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Line
	{
		public static Line CreateFromString(string @string)
		{
			// Return line
			return new Line { Texts = new OneOrManyList<SFlexibleObject<Text>> { Text.CreateFromString(@string) } };
		}

		[JsonPropertyName("iterators")] public OneOrManyList<Iterator<SFlexibleObject<Line>>?>? Iterators { get; set; }
		[JsonPropertyName("if")] public If<SFlexibleObject<Line>>? If { get; set; }
		[JsonPropertyName("special")] public string? Special { get; set; }
		[JsonPropertyName("texts")] public OneOrManyList<SFlexibleObject<Text>>? Texts { get; set; }
	}
}

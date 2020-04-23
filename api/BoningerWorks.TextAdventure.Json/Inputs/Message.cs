using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Message
	{
		public static Message CreateFromString(string @string)
		{
			// Return message
			return new Message { Lines = new OneOrManyList<SFlexibleObject<Line>> { Line.CreateFromString(@string) } };
		}

		[JsonPropertyName("lines")] public OneOrManyList<SFlexibleObject<Line>>? Lines { get; set; }
	}
}

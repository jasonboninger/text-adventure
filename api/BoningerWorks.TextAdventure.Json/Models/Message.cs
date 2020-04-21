using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Message
	{
		public static Message CreateFromString(string @string)
		{
			// Return message
			return new Message { Lines = new OneOrManyList<Line> { Line.CreateFromString(@string) } };
		}

		[JsonPropertyName("lines")] public OneOrManyList<Line> Lines { get; set; }
	}
}

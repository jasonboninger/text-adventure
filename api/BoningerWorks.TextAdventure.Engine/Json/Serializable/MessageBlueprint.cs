using BoningerWorks.TextAdventure.Engine.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Serializable
{
	public class MessageBlueprint
	{
		public static MessageBlueprint CreateFromString(string @string)
		{
			// Return message blueprint
			return new MessageBlueprint { Lines = new OneOrManyList<LineBlueprint> { new LineBlueprint { Text = @string } } };
		}

		[JsonPropertyName("lines")] public OneOrManyList<LineBlueprint> Lines { get; set; }
	}
}

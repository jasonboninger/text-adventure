using BoningerWorks.TextAdventure.Engine.Blueprints.Lines;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints.Messages
{
	public class MessageTemplatedBlueprint
	{
		[JsonPropertyName("template")] public string Template { get; set; }
		[JsonPropertyName("messages")] public Dictionary<string, OneOrManyList<MessageBlueprint>> Messages { get; set; }
		[JsonPropertyName("lines")] public Dictionary<string, OneOrManyList<LineBlueprint>> Lines { get; set; }
		[JsonPropertyName("text")] public Dictionary<string, OneOrManyList<string>> Text { get; set; }
	}
}

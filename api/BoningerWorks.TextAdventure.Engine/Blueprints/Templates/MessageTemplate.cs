using BoningerWorks.TextAdventure.Engine.Blueprints.Messages;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Blueprints.Templates
{
	public class MessageTemplate
	{
		[JsonPropertyName("template")] public OneOrManyList<MessageBlueprint> Template { get; set; }
		[JsonPropertyName("messages")] public ImmutableList<string> Messages { get; set; }
		[JsonPropertyName("lines")] public ImmutableList<string> Lines { get; set; }
		[JsonPropertyName("texts")] public ImmutableList<string> Texts { get; set; }
	}
}

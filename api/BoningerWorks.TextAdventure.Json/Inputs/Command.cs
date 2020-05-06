using BoningerWorks.TextAdventure.Json.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Command
	{
		[JsonPropertyName("id")] public string? Id { get; set; }
		[JsonPropertyName("parts")] public List<SFlexibleObject<CommandPart>>? CommandParts { get; set; }
		[JsonPropertyName("fail")] public OneOrManyList<Action?>? ActionsFail { get; set; }
	}
}

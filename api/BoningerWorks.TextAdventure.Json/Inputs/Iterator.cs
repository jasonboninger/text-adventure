using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Iterator
	{
		[JsonPropertyName("area")] public string? Area { get; set; }
		[JsonPropertyName("item")] public string? Item { get; set; }
		[JsonPropertyName("actions")] public OneOrManyList<Action?>? Actions { get; set; }
	}
}

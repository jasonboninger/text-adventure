using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Iterator<TValue>
	{
		[JsonPropertyName("area")] public string? Area { get; set; }
		[JsonPropertyName("item")] public string? Item { get; set; }
		[JsonPropertyName("processor")] public OneOrManyList<TValue>? Processor { get; set; }
	}
}

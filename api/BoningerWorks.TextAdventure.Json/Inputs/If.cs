using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class If<TValue>
	{
		[JsonPropertyName("condition")] public Condition? Condition { get; set; }
		[JsonPropertyName("true")] public OneOrManyList<TValue>? ValuesTrue { get; set; }
		[JsonPropertyName("false")] public OneOrManyList<TValue>? ValuesFalse { get; set; }
	}
}

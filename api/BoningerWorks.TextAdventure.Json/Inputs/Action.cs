using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Inputs
{
	public class Action 
	{
		[JsonPropertyName("iterators")] public OneOrManyList<Iterator?>? Iterators { get; set; }
		[JsonPropertyName("if")] public If<Action?>? If { get; set; }
		[JsonPropertyName("messages")] public OneOrManyList<SFlexibleObject<Message>>? Messages { get; set; }
		[JsonPropertyName("changes")] public OneOrManyList<Change?>? Changes { get; set; }
		[JsonPropertyName("triggers")] public OneOrManyList<Trigger?>? Triggers { get; set; }
	}
}

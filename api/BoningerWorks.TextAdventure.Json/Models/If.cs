﻿using BoningerWorks.TextAdventure.Json.Utilities;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class If<TValue>
	{
		[JsonPropertyName("condition")] public FlexibleObject<Condition> Condition { get; set; }
		[JsonPropertyName("true")] public OneOrManyList<TValue> ValuesTrue { get; set; }
		[JsonPropertyName("false")] public OneOrManyList<TValue> ValuesFalse { get; set; }
	}
}

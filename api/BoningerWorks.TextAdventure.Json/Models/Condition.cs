using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Models
{
	public class Condition
	{
		public static Condition ReadFromArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			// Create single condition
			var conditionSingle = new Condition();
			// Create many condition
			var conditionMany = new Condition { Conditions = new List<Condition>() };
			// Create index
			var index = -1;
			// Create many
			var many = false;
			// Run through array
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				// Increment index
				index++;
				// Check if first
				if (index == 0)
				{
					// Set left
					conditionSingle.Left = reader.GetString();
					// Set operator
					conditionMany.Operator = reader.GetString();
				}
				else
				{
					// Check if second
					if (index == 1)
					{
						// Set many
						many = reader.TokenType == JsonTokenType.StartObject || reader.TokenType == JsonTokenType.StartArray;
					}
					// Check index based on many
					switch (many ? 0 : index)
					{
						case 0: conditionMany.Conditions.Add(JsonSerializer.Deserialize<Condition>(ref reader, options)); break;
						case 1: conditionSingle.Comparison = reader.GetString(); break;
						case 2: conditionSingle.Right = reader.GetString(); break;
						default: throw new InvalidOperationException("Condition JSON array has too many items.");
					}
				}
			}
			// Return condition
			return many ? conditionMany : conditionSingle;
		}

		[JsonPropertyName("left")] public string Left { get; set; }
		[JsonPropertyName("comparison")] public string Comparison { get; set; }
		[JsonPropertyName("right")] public string Right { get; set; }
		[JsonPropertyName("operator")] public string Operator { get; set; }
		[JsonPropertyName("conditions")] public List<Condition> Conditions { get; set; }
	}
}

﻿using BoningerWorks.TextAdventure.Json.Static;
using BoningerWorks.TextAdventure.Json.Utilities;
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
			var conditionMany = new Condition { Conditions = new List<FlexibleObject<Condition>>() };
			// Create many
			var many = false;
			// Run through array
			for (int i = 0; reader.Read() && reader.TokenType != JsonTokenType.EndArray; i++)
			{
				// Check if first
				if (i == 0)
				{
					// Set left
					conditionSingle.Left = reader.GetString();
					// Set operator
					conditionMany.Operator = reader.GetString();
				}
				else
				{
					// Check if second
					if (i == 1)
					{
						// Set many
						many = reader.TokenType == JsonTokenType.StartObject || reader.TokenType == JsonTokenType.StartArray;
					}
					// Check index based on many
					switch (many ? 0 : i)
					{
						case 0: 
							// Add condition
							conditionMany.Conditions.Add(JsonSerializer.Deserialize<Condition>(ref reader, options));
							break;
						case 1: 
							// Set comparison
							conditionSingle.Comparison = reader.GetString(); 
							break;
						case 2: 
							// Set right
							conditionSingle.Right = reader.GetString(); 
							break;
						default: throw JsonExceptionCreator.Create(ref reader, options, "Condition array has too many items.");
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
		[JsonPropertyName("conditions")] public List<FlexibleObject<Condition>> Conditions { get; set; }
	}
}

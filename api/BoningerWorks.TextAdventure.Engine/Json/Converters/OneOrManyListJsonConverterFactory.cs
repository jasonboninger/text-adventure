using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Converters
{
	public class OneOrManyListJsonConverterFactory : JsonConverterFactory
	{
		private static readonly Type _typeOneOrManyList = typeof(OneOrManyList<>);
		private static readonly Type _typeOneOrManyListJsonConverter = typeof(OneOrManyJsonConverter<>);

		private class OneOrManyJsonConverter<TValue> : JsonConverter<OneOrManyList<TValue>>
		{
			public override OneOrManyList<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				// Create values
				var values = new OneOrManyList<TValue>();
				// Check if array
				if (reader.TokenType == JsonTokenType.StartArray)
				{
					// Run through array
					while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
					{
						// Create value
						var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
						// Add value
						values.Add(value);
					}
				}
				else
				{
					// Create value
					var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
					// Add value
					values.Add(value);
				}
				// Return values
				return values;
			}

			public override void Write(Utf8JsonWriter writer, OneOrManyList<TValue> value, JsonSerializerOptions options)
			{
				// Write list
				JsonSerializer.Serialize(writer, value == null ? null : new List<TValue>(value), options);
			}
		}

		public override bool CanConvert(Type typeToConvert)
		{
			// Check if type is one or many list
			return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition().Equals(_typeOneOrManyList);
		}

		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			// Return one or many list JSON converter
			return (JsonConverter)Activator.CreateInstance(_typeOneOrManyListJsonConverter.MakeGenericType(typeToConvert.GetGenericArguments()));
		}
	}
}

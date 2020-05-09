using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Converters
{
	public class IdDictionaryJsonConverterFactory : JsonConverterFactory
	{
		private static readonly Type _typeDictionary = typeof(Dictionary<,>);
		private static readonly Type _typeId = typeof(Id);
		private static readonly Type _typeIdDictionaryJsonConverter = typeof(IdDictionaryJsonConverter<>);

		private class IdDictionaryJsonConverter<TValue> : JsonConverter<Dictionary<Id, TValue>>
		{
			public override Dictionary<Id, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				// Create string to value mappings
				var stringToValueMappings = JsonSerializer.Deserialize<Dictionary<string, TValue>>(ref reader, options);
				// Return ID to value mappings
				return stringToValueMappings.ToDictionary(kv => new Id(kv.Key), kv => kv.Value);
			}

			public override void Write(Utf8JsonWriter writer, Dictionary<Id, TValue> value, JsonSerializerOptions options)
			{
				// Create string to value mappings
				var stringToValueMappings = value.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value);
				// Write ID to value mappings
				JsonSerializer.Serialize(writer, stringToValueMappings, options);
			}
		}

		public override bool CanConvert(Type typeToConvert)
		{
			// Check if type is dictionary and key type is ID
			return typeToConvert.IsGenericType 
				&& typeToConvert.GetGenericTypeDefinition().Equals(_typeDictionary) 
				&& typeToConvert.GetGenericArguments()[0].Equals(_typeId);
		}

		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			// Get value type
			var typeValue = typeToConvert.GetGenericArguments()[1];
			// Return ID dictionary JSON converter
			return (JsonConverter)Activator.CreateInstance(_typeIdDictionaryJsonConverter.MakeGenericType(typeValue))!;
		}
	}
}

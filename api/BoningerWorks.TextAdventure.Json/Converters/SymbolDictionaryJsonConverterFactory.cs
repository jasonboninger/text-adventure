using BoningerWorks.TextAdventure.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Converters
{
	public class SymbolDictionaryJsonConverterFactory : JsonConverterFactory
	{
		private static readonly Type _typeDictionary = typeof(Dictionary<,>);
		private static readonly Type _typeSymbol = typeof(Symbol);
		private static readonly Type _typeSymbolDictionaryJsonConverter = typeof(SymbolDictionaryJsonConverter<>);

		private class SymbolDictionaryJsonConverter<TValue> : JsonConverter<Dictionary<Symbol, TValue>>
		{
			public override Dictionary<Symbol, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				// Create string to value mappings
				var stringToValueMappings = JsonSerializer.Deserialize<Dictionary<string, TValue>>(ref reader, options);
				// Return symbol to value mappings
				return stringToValueMappings.ToDictionary(kv => new Symbol(kv.Key), kv => kv.Value);
			}

			public override void Write(Utf8JsonWriter writer, Dictionary<Symbol, TValue> value, JsonSerializerOptions options)
			{
				// Create string to value mappings
				var stringToValueMappings = value.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value);
				// Write symbol to value mappings
				JsonSerializer.Serialize(writer, stringToValueMappings, options);
			}
		}

		public override bool CanConvert(Type typeToConvert)
		{
			// Check if type is dictionary and key type is symbol
			return typeToConvert.IsGenericType 
				&& typeToConvert.GetGenericTypeDefinition().Equals(_typeDictionary) 
				&& typeToConvert.GetGenericArguments()[0].Equals(_typeSymbol);
		}

		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			// Get value type
			var typeValue = typeToConvert.GetGenericArguments()[1];
			// Return symbol dictionary JSON converter
			return (JsonConverter)Activator.CreateInstance(_typeSymbolDictionaryJsonConverter.MakeGenericType(typeValue))!;
		}
	}
}

using BoningerWorks.TextAdventure.Json.Utilities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Json.Converters
{
	internal class FlexibleObjectJsonConverter<TValue> : JsonConverter<SFlexibleObject<TValue>>
	where TValue : class
	{
		public delegate TValue Reader(ref Utf8JsonReader reader, JsonSerializerOptions options);

		private readonly Reader _readFromDefault;
		private readonly Reader _readFromString;

		public FlexibleObjectJsonConverter(Func<string, TValue>? createFromString = null)
		{
			// Set read from default
			_readFromDefault = JsonSerializer.Deserialize<TValue>;
			// Set read from string
			_readFromString = _CreateReaderOrDefault
				(
					createFromString,
					cfs => (ref Utf8JsonReader reader, JsonSerializerOptions options) => cfs(reader.GetString())
				);
		}

		public override SFlexibleObject<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// Get value
			var value = reader.TokenType switch
			{
				JsonTokenType.String => _readFromString(ref reader, options),
				_ => _readFromDefault(ref reader, options)
			};
			// Return flexible object
			return new SFlexibleObject<TValue> { Value = value };
		}

		public override void Write(Utf8JsonWriter writer, SFlexibleObject<TValue> value, JsonSerializerOptions options)
		{
			// Write value
			JsonSerializer.Serialize(writer, value.Value, options);
		}

		private Reader _CreateReaderOrDefault<TCreate>(TCreate? create, Func<TCreate, Reader> createReader)
		where TCreate : class
		{
			// Check if create does not exist
			if (create == null)
			{
				// Return default reader
				return _readFromDefault;
			}
			// Return reader
			return createReader(create);
		}
	}
}

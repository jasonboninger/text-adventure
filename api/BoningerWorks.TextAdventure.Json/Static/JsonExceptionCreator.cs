using System;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Json.Static
{
	public static class JsonExceptionCreator
	{
		public static JsonException Create(ref Utf8JsonReader reader, JsonSerializerOptions options, string message, Exception innerException = null)
		{
			// Discard reader
			_ = reader;
			// Discard options
			_ = options;
			// Check if inner exception does not exist
			if (innerException == null)
			{
				// Return exception
				return new JsonException(message);
			}
			// Return exception
			return new JsonException(message, innerException);
		}
	}
}

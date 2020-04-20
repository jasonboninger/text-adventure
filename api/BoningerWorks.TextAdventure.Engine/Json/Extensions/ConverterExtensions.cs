using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Json.Extensions
{
	public static class ConverterExtensions
	{
		public static void AddRange(this IList<JsonConverter> originals, params JsonConverter[] converters)
		{
			// Run through converters
			foreach (var converter in converters)
			{
				// Add converter
				originals.Add(converter);
			}
		}
	}
}

using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<TValue> ToEnumerable<TValue>(this TValue value)
		{
			// Return value
			yield return value;
		}
	}
}

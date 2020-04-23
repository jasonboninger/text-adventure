using System;

namespace BoningerWorks.TextAdventure.Core.Extensions
{
	public static class ExceptionExtensions
	{
		public static void ThrowIfExists<TException>(this TException? exception)
		where TException : Exception
		{
			// Check if exception exists
			if (exception != null)
			{
				// Throw error
				throw exception;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Core.Extensions
{
	public static class ActionExtensions
	{
		public static Action<TArgument> Merge<TArgument>(this IEnumerable<Action<TArgument>> actions)
		{
			// Create executables
			var executables = actions.ToArray();
			// Get length
			var length = executables.Length;
			// Return action
			return argument =>
			{
				// Run through executables
				for (int i = 0; i < length; i++)
				{
					// Execute executable
					executables[i](argument);
				}
			};
		}
	}
}

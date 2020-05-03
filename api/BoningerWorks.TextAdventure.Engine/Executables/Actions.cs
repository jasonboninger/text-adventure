using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class Actions
	{
		public static Action<ResultBuilder> Create(Entities entities, ImmutableArray<ActionMap> actionMaps)
		{
			// Create actions
			var actions = actionMaps.SelectMany(am => Action.Create(s => s, entities, am)).ToArray();
			// Get length
			var length = actions.Length;
			// Return action
			return r =>
			{
				// Run through actions
				for (int i = 0; i < length; i++)
				{
					// Execute action
					actions[i](r);
				}
			};
		}
	}
}

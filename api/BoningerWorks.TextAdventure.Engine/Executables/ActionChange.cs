using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionChange
	{
		public static Action<ResultBuilder> Create(Func<Symbol, Symbol> replacer, Entities entities, ChangeMap changeMap)
		{
			// Get path
			var path = changeMap.Path;
			// Check if metadata
			if (path.Metadata)
			{
				// Throw error
				throw new ArgumentException("Metadata cannot be changed.", nameof(changeMap));
			}
			// Get target
			var target = path.Target;
			// Replace target
			target = replacer(target);
			// Check if entity does not exist
			if (entities.TryGet(target) == null)
			{
				// Throw error
				throw new ValidationError($"Entity for symbol ({target}) could not be found.");
			}
			// Get datum
			var datum = path.Datum;
			// Get replace
			var replace = ActionReplace.Create(replacer, entities, changeMap.Value);
			// Return action
			return r =>
			{
				// Get state
				var state = r.State;
				// Get entity
				var entity = state.Entities[target];
				// Get value
				var value = replace(state);
				// Update entity
				entity = entity.UpdateData(datum, value);
				// Update state
				state = state.UpdateEntity(target, entity);
				// Set state
				r.State = state;
			};
		}
	}
}

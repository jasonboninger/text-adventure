using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionChange
	{
		public static Action<ResultBuilder> Create(Entities entities, ChangeMap changeMap)
		{
			// Get target
			var target = changeMap.Path.Target;
			// Check if entity does not exist
			if (entities.TryGet(target) == null)
			{
				// Throw error
				throw new ValidationError($"Entity for symbol ({target}) could not be found.");
			}
			// Get datum
			var datum = changeMap.Path.Datum;
			// Check if custom
			if (changeMap.Path.Custom)
			{
				// Get replaceable
				var replaceable = new Replaceable(entities, changeMap.Value);
				// Return execute
				return result =>
				{
					// Get state
					var state = result.State;
					// Get entity
					var entity = state.Entities[target];
					// Get value
					var value = replaceable.Replace(state);
					// Update entity
					entity = entity.UpdateCustomData(datum, value);
					// Update state
					state = state.UpdateEntity(target, entity);
					// Set state
					result.State = state;
				};
			}
			else
			{
				// Create value
				var value = Symbol.TryCreate(changeMap.Value) ?? throw new ValidationError($"Data value ({changeMap.Value}) is not a valid symbol.");
				// Ensure valid data
				entities.Get(target).EnsureValidData(datum, value);
				// Return execute
				return result =>
				{
					// Get state
					var state = result.State;
					// Get entity
					var entity = state.Entities[target];
					// Update entity
					entity = entity.UpdateData(datum, value);
					// Update state
					state = state.UpdateEntity(target, entity);
					// Set state
					result.State = state;
				};
			}
		}
	}
}

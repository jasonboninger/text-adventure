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
			// Get target symbol
			var targetSymbol = changeMap.TargetSymbol;
			// Check if entity does not exist
			if (!entities.Contains(targetSymbol))
			{
				// Throw error
				throw new ValidationError($"Entity for symbol ({targetSymbol}) could not be found.");
			}
			// Get target datum
			var targetDatum = changeMap.TargetDatum;
			// Check if custom datum
			if (changeMap.CustomDatum)
			{
				// Get new value
				var newValue = changeMap.NewValue;
				// Return execute
				return result =>
				{
					// Get state
					var state = result.State;
					// Get entity
					var entity = state.Entities[targetSymbol];
					// Update entity
					entity = entity.UpdateCustomData(targetDatum, newValue);
					// Update state
					state = state.UpdateEntity(targetSymbol, entity);
					// Set state
					result.State = state;
				};
			}
			else
			{
				// Create new value
				var newValue = Symbol.TryCreate(changeMap.NewValue) 
					?? throw new ValidationError($"Data value ({changeMap.NewValue}) is not a valid symbol.");
				// Get entity
				var entity = entities.Get(targetSymbol);
				// Ensure valid data
				entity.EnsureValidData(targetDatum, newValue);
				// Return execute
				return result =>
				{
					// Get state
					var state = result.State;
					// Get entity
					var entity = state.Entities[targetSymbol];
					// Update entity
					entity = entity.UpdateData(targetDatum, newValue);
					// Update state
					state = state.UpdateEntity(targetSymbol, entity);
					// Set state
					result.State = state;
				};
			}
		}
	}
}

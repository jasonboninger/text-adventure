using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionChange
	{
		public static Action<ResultBuilder> Create(Player player, Areas areas, Items items, ChangeMap changeMap)
		{
			// Get target symbol
			var targetSymbol = changeMap.TargetSymbol;
			// Check if not player, area, or item
			if (targetSymbol != player.Symbol && !areas.Contains(targetSymbol) && !items.Contains(targetSymbol))
			{
				// Throw error
				throw new ValidationError($"Target symbol ({targetSymbol}) could not be found.");
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
					?? throw new ValidationError($"Data value ({changeMap.NewValue}) is not a valid.");
				// Throw if not valid data
				_ThrowIfNotValidData(player, areas, items, targetSymbol, targetDatum, newValue);
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

		private static void _ThrowIfNotValidData(Player player, Areas areas, Items items, Symbol targetSymbol, Symbol targetDatum, Symbol newValue)
		{
			// Check if player
			if (targetSymbol == player.Symbol)
			{
				// Throw error
				throw new ValidationError($"Player data ({targetDatum}) could not be found.");
			}
			// Check if area
			if (areas.Contains(targetSymbol))
			{
				// Throw error
				throw new ValidationError($"Area data ({targetDatum}) could not be found.");
			}
			// Check if item
			if (items.Contains(targetSymbol))
			{
				// Check if active
				if (targetDatum == Item.DatumActive)
				{
					// Check if not true or false
					if (newValue != Game.ValueTrue && newValue != Game.ValueFalse)
					{
						// Throw error
						throw new ValidationError($"Item active ({newValue}) must be set to {Game.ValueTrue} or {Game.ValueFalse}.");
					}
					// Return
					return;
				}
				// Check if location
				if (targetDatum == Item.DatumLocation)
				{
					// Check if not player and area does not exist
					if (targetDatum != player.Symbol && !areas.Contains(targetDatum))
					{
						// Throw error
						throw new ValidationError($"Item location ({newValue}) must be set to {player.Symbol} or an area.");
					}
					// Return
					return;
				}
				// Throw error
				throw new ValidationError($"Item data ({targetDatum}) could not be found.");
			}
		}
	}
}

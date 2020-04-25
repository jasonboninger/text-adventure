﻿using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Maps.Models
{
	public class AreaMap
	{
		public Symbol AreaSymbol { get; }
		public ImmutableArray<ItemMap> ItemMaps { get; }
		public ImmutableArray<ReactionMap> ReactionMaps { get; }

		internal AreaMap(string areaSymbol, Area? area)
		{
			// Set area symbol
			AreaSymbol = Symbol.TryCreate(areaSymbol) ?? throw new ValidationError($"Area symbol ({areaSymbol}) is not valid.");
			// Try to create area
			try
			{
				// Check if area does not exist
				if (area == null)
				{
					// Throw error
					throw new ValidationError("Area body cannot be null.");
				}
				// Set item maps
				ItemMaps = ItemMap.Create(area.ItemSymbolToItemMappings, AreaSymbol);
				// Set reaction maps
				ReactionMaps = ReactionMap.Create(area.Reactions, itemSymbolDefault: null);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Area ({AreaSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

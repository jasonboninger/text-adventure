﻿using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class AreaMap
	{
		public Symbol AreaSymbol { get; }
		public Names AreaNames { get; }

		internal ImmutableArray<ItemMap> ItemMaps { get; }
		internal ImmutableArray<ReactionMap> ReactionMaps { get; }

		internal AreaMap(Area? area)
		{
			// Check if area does not exist
			if (area == null)
			{
				// Throw error
				throw new ValidationError("Area cannot be null.");
			}
			// Set area symbol
			AreaSymbol = Symbol.TryCreate(area.Id) ?? throw new ValidationError($"Area symbol ({area.Id}) is not valid.");
			// Try to create area
			try
			{
				// Set area names
				AreaNames = Names.TryCreate(area.Names?.Select(n => Name.TryCreate(n) ?? throw new ValidationError($"Name ({n}) is not valid.")))
					?? throw new ValidationError("Names is not valid.");
				// Set item maps
				ItemMaps = ItemMap.Create(area.Items, AreaSymbol);
				// Set reaction maps
				ReactionMaps = ReactionMap.Create(AreaSymbol, area.Reactions);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Area ({AreaSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ItemMap
	{
		internal static ImmutableArray<ItemMap> Create(List<Item?>? items, Id locationId)
		{
			// Check if items does not exist
			if (items == null)
			{
				// Return no item maps
				return ImmutableArray<ItemMap>.Empty;
			}
			// Create item maps
			var itemMaps = items.Select(i => new ItemMap(locationId, i)).ToImmutableArray();
			// Return item maps
			return itemMaps;
		}

		public Id ItemId { get; }
		public Id LocationId { get; }
		public Names ItemNames { get; }
		public bool? Active { get; }
		
		internal ImmutableArray<ItemMap> ItemMaps { get; }
		internal ImmutableArray<ReactionMap> ReactionMaps { get; }

		private ItemMap(Id locationId, Item? item)
		{
			// Check if item does not exist
			if (item == null)
			{
				// Throw error
				throw new ValidationError("Item body cannot be null.");
			}
			// Set item ID
			ItemId = Id.TryCreate(item.Id) ?? throw new ValidationError($"Item ID ({item.Id}) is not valid.");
			// Try to create item map
			try
			{
				// Set location ID
				LocationId = locationId ?? throw new ValidationError("Location ID cannot be null.");
				// Check if names does not exist
				if (item.Names == null || item.Names.Count == 0)
				{
					// Throw error
					throw new ValidationError("Names cannot be null or empty.");
				}
				// Set item names
				ItemNames = Names.TryCreate(item.Names?.Select(n => Name.TryCreate(n) ?? throw new ValidationError($"Name ({n}) is not valid.")))
					?? throw new ValidationError("Names is not valid.");
				// Set active
				Active = item.Active;
				// Create item maps
				var itemMaps = Create(item.Items, LocationId);
				// Set item maps
				ItemMaps = itemMaps.Concat(itemMaps.SelectMany(im => im.ItemMaps)).ToImmutableArray();
				// Set reaction maps
				ReactionMaps = ReactionMap.Create(ItemId, item.Reactions);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Item ({ItemId}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

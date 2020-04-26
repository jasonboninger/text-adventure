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
		internal static ImmutableArray<ItemMap> Create(Dictionary<string, Item?>? itemSymbolToItemMappings, Symbol locationSymbol)
		{
			// Check if item symbol to item mappings does not exist
			if (itemSymbolToItemMappings == null)
			{
				// Return no item maps
				return ImmutableArray<ItemMap>.Empty;
			}
			// Create item maps
			var itemMaps = itemSymbolToItemMappings.Select(kv => new ItemMap(kv.Key, locationSymbol, kv.Value)).ToImmutableArray();
			// Return item maps
			return itemMaps;
		}

		public Symbol ItemSymbol { get; }
		public Symbol LocationSymbol { get; }
		public Names ItemNames { get; }
		public Name ItemName { get; }
		public bool? Active { get; }
		
		internal ImmutableArray<ItemMap> ItemMaps { get; }
		internal ImmutableArray<ReactionMap> ReactionMaps { get; }

		internal ItemMap(string itemSymbol, Symbol locationSymbol, Item? item)
		{
			// Set item symbol
			ItemSymbol = Symbol.TryCreate(itemSymbol) ?? throw new ValidationError($"Item symbol ({itemSymbol}) is not valid.");
			// Try to create item map
			try
			{
				// Set location symbol
				LocationSymbol = locationSymbol ?? throw new ValidationError("Location symbol cannot be null.");
				// Check if item does not exist
				if (item == null)
				{
					// Throw error
					throw new ValidationError("Item body cannot be null.");
				}
				// Check if names does not exist
				if (item.Names == null || item.Names.Count == 0)
				{
					// Throw error
					throw new ValidationError("Names cannot be null or empty.");
				}
				// Set item names
				ItemNames = Names.TryCreate(item.Names.Select(n => Name.TryCreate(n) ?? throw new ValidationError($"Name ({n}) is not valid.")))
					?? throw new ValidationError("Names is not valid.");
				// Set item name
				ItemName = ItemNames.First();
				// Set active
				Active = item.Active;
				// Create item maps
				var itemMaps = Create(item.ItemSymbolToItemMappings, LocationSymbol);
				// Set item maps
				ItemMaps = itemMaps.Concat(itemMaps.SelectMany(im => im.ItemMaps)).ToImmutableArray();
				// Set reaction maps
				ReactionMaps = item.Reactions?.Select(r => new ReactionMap(r, ItemSymbol)).ToImmutableArray()
					?? ImmutableArray<ReactionMap>.Empty;
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Item ({itemSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

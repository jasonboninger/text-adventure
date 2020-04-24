using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Maps.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Maps.Maps
{
	public class ItemMap
	{
		public Symbol ItemSymbol { get; }
		public Symbol LocationSymbol { get; }
		public Names ItemNames { get; }
		public bool? Active { get; }
		public ImmutableArray<ReactionMap> ReactionMaps { get; }
		public ImmutableArray<ItemMap> ItemMaps { get; }

		public ItemMap(string itemSymbol, Symbol locationSymbol, Item? item)
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
				// Set active
				Active = item.Active;
				// Set reaction maps
				ReactionMaps = item.Reactions?.Select(r => new ReactionMap(r, ItemSymbol)).ToImmutableArray() 
					?? ImmutableArray<ReactionMap>.Empty;
				// Set item maps
				ItemMaps = item.ItemSymbolToItemMappings?.Select(kv => new ItemMap(kv.Key, LocationSymbol, kv.Value)).ToImmutableArray()
					?? ImmutableArray<ItemMap>.Empty;
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Item ({itemSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

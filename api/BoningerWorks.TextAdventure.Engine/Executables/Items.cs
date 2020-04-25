using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Items : IReadOnlyList<Item>
	{
		public int Count => _items.Length;
		public Item this[int index] => _items[index];

		public string RegularExpression { get; }

		private readonly ImmutableArray<Item> _items;
		private readonly IEnumerable<Item> _itemsEnumerable;
		private readonly ImmutableDictionary<Symbol, Item> _itemSymbolToItemMappings;
		private readonly ImmutableDictionary<Name, ImmutableArray<Item>> _itemNameToItemsMappings;

		public Items(ImmutableArray<ItemMap> itemMaps)
		{
			// Set items
			_items = itemMaps.Select(im => new Item(im)).ToImmutableArray();
			// Set enumerable items
			_itemsEnumerable = _items;
			// Create item symbol to item mappings
			_itemSymbolToItemMappings = _items.ToImmutableDictionary(i => i.Symbol);
			// Create item name to items mappings
			_itemNameToItemsMappings = _items
				.SelectMany(i => i.Names, (i, n) => new { Item = i, Name = n })
				.GroupBy(_ => _.Name, _ => _.Item)
				.ToImmutableDictionary(g => g.Key, g => g.ToImmutableArray());
			// Set regular expression
			RegularExpression = string.Join(@"|", _items.Select(i => RegularExpressions.CreateNonCapturingGroup(i.RegularExpression)));
		}

		public IEnumerator<Item> GetEnumerator() => _itemsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _itemsEnumerable.GetEnumerator();

		public Item Get(Symbol symbol)
		{
			// Try to get item
			if (symbol == null || !_itemSymbolToItemMappings.TryGetValue(symbol, out var item))
			{
				// Throw error
				throw new ArgumentException($"No item with symbol ({symbol}) could be found.");
			}
			// Return item
			return item;
		}
		public Item Get(Name name)
		{
			// Try to get items
			if (name == null || !_itemNameToItemsMappings.TryGetValue(name, out var items))
			{
				// Throw error
				throw new ArgumentException($"No item with name ({name}) could be found.");
			}
			// Check if more than one item exists
			if (items.Length > 1)
			{
				// Throw error
				throw GenericException.Create(new AmbiguousItemMatchError(name, items));
			}
			// Return item
			return items[0];
		}

		public bool Contains(Symbol symbol)
		{
			// Check if symbol does not exist
			if (symbol == null)
			{
				// Return item does not exist
				return false;
			}
			// Return if item with symbol exists
			return _itemSymbolToItemMappings.ContainsKey(symbol);
		}
		public bool Contains(Name name)
		{
			// Check if name does not exist
			if (name == null)
			{
				// Return items do not exist
				return false;
			}
			// Return if items with name exist
			return _itemNameToItemsMappings.ContainsKey(name);
		}
	}
}

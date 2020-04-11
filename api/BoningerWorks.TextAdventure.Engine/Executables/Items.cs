using BoningerWorks.TextAdventure.Engine.Static;
using BoningerWorks.TextAdventure.Engine.Utilities;
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
		public Item this[Symbol symbol]
		{
			get
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
		}
		public ImmutableArray<Item> this[Name name]
		{
			get
			{
				// Try to get items
				if (name == null || !_itemNameToItemsMappings.TryGetValue(name, out var items))
				{
					// Throw error
					throw new ArgumentException($"No items with name ({name}) could be found.");
				}
				// Return items
				return items;
			}
		}

		public string RegularExpression { get; }

		private readonly ImmutableArray<Item> _items;
		private readonly IEnumerable<Item> _itemsEnumerable;
		private readonly ImmutableDictionary<Symbol, Item> _itemSymbolToItemMappings;
		private readonly ImmutableDictionary<Name, ImmutableArray<Item>> _itemNameToItemsMappings;

		public Items(IEnumerable<Item> items) : this(items?.ToImmutableArray()) { }
		public Items(ImmutableArray<Item> items) : this((ImmutableArray<Item>?)items) { }
		private Items(ImmutableArray<Item>? items)
		{
			// Set items
			_items = items ?? throw new ArgumentException("Items cannot be null.", nameof(items));
			// Set enumerable items
			_itemsEnumerable = _items;
			// Check if not all items exist
			if (_items.Any(i => i == null))
			{
				// Throw error
				throw new ArgumentException("No items can be null", nameof(items));
			}
			// Check if not all item names are unique
			if (_items.Select(i => i.Name).Distinct().Count() != _items.Length)
			{
				// Throw error
				throw new ArgumentException("Not all item names are unique.", nameof(items));
			}
			// Create item symbol to item mappings
			_itemSymbolToItemMappings = _items.ToImmutableDictionary(i => i.Symbol);
			// Create item name to items mappings
			_itemNameToItemsMappings = _items
				.SelectMany(i => i.Names, (i, n) => new { Item = i, Name = n })
				.GroupBy(_ => _.Name, _ => _.Item)
				.ToImmutableDictionary(g => g.Key, g => g.Distinct().ToImmutableArray());
			// Set regular expression
			RegularExpression = _CreateRegularExpression(_items);
		}

		public IEnumerator<Item> GetEnumerator() => _itemsEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _itemsEnumerable.GetEnumerator();

		private static string _CreateRegularExpression(ImmutableArray<Item> items)
		{
			// Create regular expression
			var regularExpression = string.Join(@"|", items.Select(i => RegularExpressions.CreateNonCapturingGroup(i.RegularExpression)));
			// Return regular expression
			return regularExpression;
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Exceptions;
using BoningerWorks.TextAdventure.Engine.Exceptions.Data;
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
		public Item this[Name name]
		{
			get
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
					throw GenericException.Create(new AmbiguousItemMatchData(name, items));
				}
				// Return item
				return items[0];
			}
		}

		public string RegularExpression { get; }

		private readonly ImmutableArray<Item> _items;
		private readonly IEnumerable<Item> _itemsEnumerable;
		private readonly ImmutableDictionary<Symbol, Item> _itemSymbolToItemMappings;
		private readonly ImmutableDictionary<Name, ImmutableArray<Item>> _itemNameToItemsMappings;

		public Items(IEnumerable<Item> items) : this(items.ToImmutableArray()) { }
		public Items(ImmutableArray<Item> items)
		{
			// Check if not all items exist
			if (items.Any(i => i == null))
			{
				// Throw error
				throw new ArgumentException("No items can be null", nameof(items));
			}
			// Check if not all item symbols are unique
			if (items.Select(i => i.Symbol).Distinct().Count() != items.Length)
			{
				// Throw error
				throw new ArgumentException("Not all item symbols are unique.", nameof(items));
			}
			// Check if not all item names are unique
			if (items.Select(i => i.Name).Distinct().Count() != items.Length)
			{
				// Throw error
				throw new ArgumentException("Not all item names are unique.", nameof(items));
			}
			// Set items
			_items = items;
			// Set enumerable items
			_itemsEnumerable = _items;
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

		public bool ExistsItem(Symbol symbol)
		{
			// Return if item symbol exists
			return _itemSymbolToItemMappings.ContainsKey(symbol);
		}

		public bool ExistsName(Name name)
		{
			// Return if item name exists
			return _itemNameToItemsMappings.ContainsKey(name);
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

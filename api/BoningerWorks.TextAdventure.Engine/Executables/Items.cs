using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Items : IReadOnlyList<Item>
	{
		public Item this[int index] => _items[index];
		public int Count => _items.Count;
		public string RegularExpression => _items.RegularExpression;

		private readonly GroupNamed<Item> _items;

		public Items(Player player, Areas areas, ImmutableArray<ItemMap> itemMaps)
		{
			// Set items
			_items = new GroupNamed<Item>(itemMaps.Select(im => new Item(player, areas, im)));
		}

		IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
		public IEnumerator<Item> GetEnumerator() => _items.GetEnumerator();

		public Item Get(Symbol symbol) => _items.Get(symbol);
		public Item Get(Name name) => _items.Get(name);

		public Item? TryGet(Symbol symbol) => _items.TryGet(symbol);

		public ImmutableArray<Item> GetAll(Name name) => _items.GetAll(name);

		public ImmutableArray<Item>? TryGetAll(Name? name) => _items.TryGetAll(name);

		public bool Contains(Symbol? symbol) => _items.Contains(symbol);
		public int Contains(Name? name) => _items.Contains(name);
	}
}

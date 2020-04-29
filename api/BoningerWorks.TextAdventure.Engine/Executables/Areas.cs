using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Areas : IReadOnlyList<Area>
	{
		public Area this[int index] => _areas[index];
		public int Count => _areas.Length;

		private readonly ImmutableArray<Area> _areas;
		private readonly IEnumerable<Area> _areasEnumerable;
		private readonly ImmutableDictionary<Symbol, Area> _symbolToAreaMappings;

		public Areas(ImmutableArray<AreaMap> areaMaps)
		{
			// Set areas
			_areas = areaMaps.Select(am => new Area(am)).ToImmutableArray();
			// Set enumerable areas
			_areasEnumerable = _areas;
			// Set symbol to area mappings
			_symbolToAreaMappings = _areas.ToImmutableDictionary(a => a.Symbol);
		}

		public bool Contains(Symbol? symbol)
		{
			// Return if area exists
			return symbol != null && _symbolToAreaMappings.ContainsKey(symbol);
		}

		public IEnumerator<Area> GetEnumerator() => _areasEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _areasEnumerable.GetEnumerator();
	}
}

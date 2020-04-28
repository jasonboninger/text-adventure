using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Areas : IReadOnlyList<Symbol>
	{
		public Symbol this[int index] => _areas[index];
		public int Count => _areas.Length;

		private readonly ImmutableArray<Symbol> _areas;
		private readonly IEnumerable<Symbol> _areasEnumerable;

		public Areas(ImmutableArray<AreaMap> areaMaps)
		{
			// Set areas
			_areas = areaMaps.Select(am => am.AreaSymbol).ToImmutableArray();
			// Set enumerable areas
			_areasEnumerable = _areas;
		}

		public bool Contains(Symbol? symbol)
		{
			// Return if area exists
			return symbol != null && _areas.Contains(symbol);
		}

		public IEnumerator<Symbol> GetEnumerator() => _areasEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _areasEnumerable.GetEnumerator();
	}
}

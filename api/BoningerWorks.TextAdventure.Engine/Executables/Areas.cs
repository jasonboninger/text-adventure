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
		public int Count => _areas.Count;
		public string RegularExpression => _areas.RegularExpression;

		private readonly GroupNamed<Area> _areas;

		public Areas(ImmutableArray<AreaMap> areaMaps)
		{
			// Set areas
			_areas = new GroupNamed<Area>(areaMaps.Select(am => new Area(am)));
		}

		IEnumerator IEnumerable.GetEnumerator() => _areas.GetEnumerator();
		public IEnumerator<Area> GetEnumerator() => _areas.GetEnumerator();

		public Area Get(Symbol symbol) => _areas.Get(symbol);
		public Area Get(Name name) => _areas.Get(name);

		public Area? TryGet(Symbol symbol) => _areas.TryGet(symbol);

		public ImmutableArray<Area> GetAll(Name name) => _areas.GetAll(name);

		public ImmutableArray<Area>? TryGetAll(Name? name) => _areas.TryGetAll(name);

		public bool Contains(Symbol? symbol) => _areas.Contains(symbol);
		public int Contains(Name? name) => _areas.Contains(name);
	}
}

﻿using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Structural
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
			_areas = new GroupNamed<Area>(areaMaps.Select(am => new Area(am)).OrderBy(a => a.Names.Name));
		}

		IEnumerator IEnumerable.GetEnumerator() => _areas.GetEnumerator();
		public IEnumerator<Area> GetEnumerator() => _areas.GetEnumerator();

		public Area Get(Id id) => _areas.Get(id);
		public Area Get(Name name) => _areas.Get(name);

		public Area? TryGet(Id id) => _areas.TryGet(id);

		public ImmutableArray<Area> GetAll(Name name) => _areas.GetAll(name);

		public ImmutableArray<Area>? TryGetAll(Name? name) => _areas.TryGetAll(name);

		public bool Contains(Id? id) => _areas.Contains(id);
		public int Contains(Name? name) => _areas.Contains(name);
	}
}

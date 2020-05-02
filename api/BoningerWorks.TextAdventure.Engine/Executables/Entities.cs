using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Entities : IReadOnlyList<IEntity>
	{
		public IEntity this[int index] => _entities[index];
		public int Count => _entities.Count;

		private readonly Group<IEntity> _entities;

		public Entities(Player player, Areas areas, Items items)
		{
			// Set entities
			_entities = new Group<IEntity>(Enumerable.Empty<IEntity>().Append(player).Concat(areas).Concat(items));
		}

		IEnumerator IEnumerable.GetEnumerator() => _entities.GetEnumerator();
		public IEnumerator<IEntity> GetEnumerator() => _entities.GetEnumerator();

		public IEntity Get(Symbol symbol) => _entities.Get(symbol);

		public IEntity? TryGet(Symbol? symbol) => _entities.TryGet(symbol);
	}
}

using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Structural
{
	public class Entities : IReadOnlyList<IEntity>
	{
		public IEntity this[int index] => _entities[index];
		public int Count => _entities.Count;

		public Player Player { get; }
		public Areas Areas { get; }
		public Items Items { get; }

		private readonly Group<IEntity> _entities;

		public Entities(Player player, Areas areas, Items items)
		{
			// Set player
			Player = player;
			// Set areas
			Areas = areas;
			// Set items
			Items = items;
			// Set entities
			_entities = new Group<IEntity>(Enumerable.Empty<IEntity>().Append(Player).Concat(Areas).Concat(Items));
		}

		IEnumerator IEnumerable.GetEnumerator() => _entities.GetEnumerator();
		public IEnumerator<IEntity> GetEnumerator() => _entities.GetEnumerator();

		public IEntity Get(Id id) => _entities.Get(id);

		public IEntity? TryGet(Id? id) => _entities.TryGet(id);
	}
}

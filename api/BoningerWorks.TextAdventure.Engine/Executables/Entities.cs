using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Entities : IReadOnlyList<IEntity>
	{
		public int Count => _entities.Length;
		public IEntity this[int index] => _entities[index];

		private readonly ImmutableArray<IEntity> _entities;
		private readonly IEnumerable<IEntity> _entitiesEnumerable;
		private readonly ImmutableDictionary<Symbol, IEntity> _symbolToEntityMappings;

		public Entities(Player player, Areas areas, Items items)
		{
			// Set entities
			_entities = Enumerable.Empty<IEntity>().Append(player).Concat(areas).Concat(items).ToImmutableArray();
			// Set enumerable entities
			_entitiesEnumerable = _entities;
			// Set symbol to entity mappings
			_symbolToEntityMappings = _entities.ToImmutableDictionary(e => e.Symbol);
		}

		IEnumerator IEnumerable.GetEnumerator() => _entitiesEnumerable.GetEnumerator();
		public IEnumerator<IEntity> GetEnumerator() => _entitiesEnumerable.GetEnumerator();

		public IEntity Get(Symbol symbol)
		{
			// Return entity
			return TryGet(symbol) ?? throw new ArgumentException($"No entity with symbol ({symbol}) could be found.");
		}

		public IEntity? TryGet(Symbol? symbol)
		{
			// Try to get entity
			if (symbol == null || !_symbolToEntityMappings.TryGetValue(symbol, out var entity))
			{
				// Return no entity
				return null;
			}
			// Return entity
			return entity;
		}
	}
}

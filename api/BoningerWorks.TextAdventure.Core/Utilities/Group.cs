using BoningerWorks.TextAdventure.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public class Group<TValue> : IReadOnlyList<TValue>
	where TValue : class, IIdentifiable
	{
		public int Count => _values.Length;
		public TValue this[int index] => _values[index];

		private readonly ImmutableArray<TValue> _values;
		private readonly IEnumerable<TValue> _valuesEnumerable;
		private readonly ImmutableDictionary<Id, TValue> _idToValueMappings;

		public Group(IEnumerable<TValue> values)
		{
			// Set values
			_values = values?.ToImmutableArray() ?? throw new ArgumentException("Values cannot be null.", nameof(values));
			// Check if any values do not exist
			if (_values.Any(v => v == null))
			{
				// Throw error
				throw new ArgumentException("Value cannot be null.", nameof(values));
			}
			// Check if any ID does not exist
			if (_values.Any(v => v.Id == null))
			{
				// Throw error
				throw new ArgumentException("Value ID cannot be null.", nameof(values));
			}
			// Set enumerable values
			_valuesEnumerable = _values;
			// Create ID to value mappings
			_idToValueMappings = _values.ToImmutableDictionary(i => i.Id);
		}

		IEnumerator IEnumerable.GetEnumerator() => _valuesEnumerable.GetEnumerator();
		public IEnumerator<TValue> GetEnumerator() => _valuesEnumerable.GetEnumerator();

		public TValue Get(Id id)
		{
			// Return value
			return TryGet(id) ?? throw new ArgumentException($"ID ({id}) does not match any values in the group.");
		}

		public TValue? TryGet(Id? id)
		{
			// Try to get value
			if (id != null && _idToValueMappings.TryGetValue(id, out var value))
			{
				// Return value
				return value;
			}
			// Return no value
			return null;
		}

		public bool Contains(Id? id)
		{
			// Check if ID does not exist
			if (id == null)
			{
				// Return value does not exist
				return false;
			}
			// Return if value with ID exists
			return _idToValueMappings.ContainsKey(id);
		}
	}
}

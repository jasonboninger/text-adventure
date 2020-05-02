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
		private readonly ImmutableDictionary<Symbol, TValue> _symbolToValueMappings;

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
			// Check if any symbol does not exist
			if (_values.Any(v => v.Symbol == null))
			{
				// Throw error
				throw new ArgumentException("Value symbol cannot be null.", nameof(values));
			}
			// Set enumerable values
			_valuesEnumerable = _values;
			// Create symbol to value mappings
			_symbolToValueMappings = _values.ToImmutableDictionary(i => i.Symbol);
		}

		IEnumerator IEnumerable.GetEnumerator() => _valuesEnumerable.GetEnumerator();
		public IEnumerator<TValue> GetEnumerator() => _valuesEnumerable.GetEnumerator();

		public TValue Get(Symbol symbol)
		{
			// Return value
			return TryGet(symbol) ?? throw new ArgumentException($"Symbol ({symbol}) does not match any values in the group.");
		}

		public TValue? TryGet(Symbol? symbol)
		{
			// Try to get value
			if (symbol != null && _symbolToValueMappings.TryGetValue(symbol, out var value))
			{
				// Return value
				return value;
			}
			// Return no value
			return null;
		}

		public bool Contains(Symbol? symbol)
		{
			// Check if symbol does not exist
			if (symbol == null)
			{
				// Return value does not exist
				return false;
			}
			// Return if value with symbol exists
			return _symbolToValueMappings.ContainsKey(symbol);
		}
	}
}

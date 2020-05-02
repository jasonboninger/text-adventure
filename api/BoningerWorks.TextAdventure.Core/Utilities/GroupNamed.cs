using BoningerWorks.TextAdventure.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public class GroupNamed<TValue> : Group<TValue>
	where TValue : class, INamed
	{
		public string RegularExpression { get; }

		private readonly ImmutableDictionary<Name, ImmutableArray<TValue>> _nameToValuesMappings;

		public GroupNamed(IEnumerable<TValue> values) : base(values)
		{
			// Check if any names does not exist
			if (this.Any(v => v.Names == null))
			{
				// Throw error
				throw new ArgumentException("Value names cannot be null.", nameof(values));
			}
			// Run through values
			foreach (var value in this)
			{
				// Check if not every value name is unique
				if (this.Any(v => v != value && v.Names.Contains(value.Names.Name)))
				{
					// Throw error
					throw new ArgumentException($"Value name ({value.Names.Name}) is not unique in the group.", nameof(values));
				}
			}
			// Create name to values mappings
			_nameToValuesMappings = this
				.SelectMany(v => v.Names, (v, n) => new { Value = v, Name = n })
				.GroupBy(_ => _.Name, _ => _.Value)
				.ToImmutableDictionary(g => g.Key, g => g.ToImmutableArray());
			// Set regular expression
			RegularExpression = new Names(this.SelectMany(v => v.Names)).RegularExpression;
		}

		public TValue Get(Name name)
		{
			// Get values
			var values = GetAll(name);
			// Check if more than one value
			if (values.Length > 1)
			{
				// Throw error
				throw new ArgumentException($"Name ({name}) returned more than one value in the group.");
			}
			// Return value
			return values[0];
		}
		
		public ImmutableArray<TValue> GetAll(Name name)
		{
			// Return values
			return TryGetAll(name) ?? throw new ArgumentException($"Name ({name}) does not match any values in the group.");
		}

		public ImmutableArray<TValue>? TryGetAll(Name? name)
		{
			// Try to get values
			if (name != null && _nameToValuesMappings.TryGetValue(name, out var values))
			{
				// Return values
				return values;
			}
			// Return no values
			return null;
		}

		public int Contains(Name? name)
		{
			// Return count
			return name == null || !_nameToValuesMappings.TryGetValue(name, out var values) ? 0 : values.Length;
		}
	}
}

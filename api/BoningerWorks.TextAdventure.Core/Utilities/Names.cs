using BoningerWorks.TextAdventure.Core.Static;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public class Names : IReadOnlyList<Name>
	{
		public static Names? TryCreate(IEnumerable<Name>? names)
		{
			// Create value
			Names? value;
			// Create try
			var @try = true;
			// Try to create names
			try
			{
				// Set names
				value = new Names(names!);
				// Return names
				return value;
			}
			catch
			{
				// Set no names
				value = null;
				// Check if try
				if (@try)
				{
					// Return no names
					return value;
				}
				// Throw error
				throw;
			}
		}

		public int Count => _names.Length;
		public Name this[int index] => _names[index];

		public Name Name { get; }
		public string RegularExpression { get; }

		private readonly ImmutableArray<Name> _names;
		private readonly IEnumerable<Name> _namesEnumerable;

		public Names(IEnumerable<Name> names)
		{
			// Set names
			_names = names?.Distinct().ToImmutableArray() ?? ImmutableArray<Name>.Empty;
			// Set enumerable names
			_namesEnumerable = _names;
			// Check if no names exist
			if (_names.Length == 0)
			{
				// Throw error
				throw new ArgumentException("At least one name is required.", nameof(names));
			}
			// Check if any names do not exist
			if (_names.Any(n => n == null))
			{
				// Throw error
				throw new ArgumentException("No names can be null.", nameof(names));
			}
			// Set name
			Name = _names.First();
			// Set regular expression
			RegularExpression = _CreateRegularExpression(_names);
		}

		private static string _CreateRegularExpression(ImmutableArray<Name> names)
		{
			// Create regular expression
			var regularExpression = string.Join(@"|", names.Select(n => RegularExpressions.CreateNonCapturingGroup(n.RegularExpression)));
			// Return regular expression
			return regularExpression;
		}

		IEnumerator IEnumerable.GetEnumerator() => _namesEnumerable.GetEnumerator();
		public IEnumerator<Name> GetEnumerator() => _namesEnumerable.GetEnumerator();
	}
}

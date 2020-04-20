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
		public int Count => _names.Length;
		public Name this[int index] => _names[index];

		public string RegularExpression { get; }

		private readonly ImmutableArray<Name> _names;
		private readonly IEnumerable<Name> _namesEnumerable;

		public Names(IEnumerable<Name> names) : this(names.ToImmutableArray()) { }
		public Names(ImmutableArray<Name> names)
		{
			// Check if no names exist
			if (names.Length == 0)
			{
				// Throw error
				throw new ArgumentException("At least one name is required.", nameof(names));
			}
			// Set names
			_names = names;
			// Set enumerable names
			_namesEnumerable = _names;
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

		public IEnumerator<Name> GetEnumerator() => _namesEnumerable.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _namesEnumerable.GetEnumerator();
	}
}

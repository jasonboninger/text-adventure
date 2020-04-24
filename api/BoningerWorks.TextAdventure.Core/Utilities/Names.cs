using BoningerWorks.TextAdventure.Core.Extensions;
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
		public static Names? TryCreate(IEnumerable<Name> names)
		{
			// Create names object
			Names? namesObject;
			// Create names array
			var namesArray = names?.ToImmutableArray();
			// Check if names array exists and exception does not exist
			if (namesArray.HasValue && _GetException(namesArray) == null)
			{
				// Set names object
				namesObject = new Names(namesArray.Value);
			}
			else
			{
				// Set no names object
				namesObject = null;
			}
			// Return no names
			return namesObject;
		}

		private static Exception? _GetException(ImmutableArray<Name>? names)
		{
			// Check if no names exist
			if (!names.HasValue || names.Value.Length == 0)
			{
				// Return error
				return new ArgumentException("At least one name is required.", nameof(names));
			}
			// Check if any names do not exist
			if (names.Value.Any(n => n == null))
			{
				// Return error
				return new ArgumentException("No names can be null.", nameof(names));
			}
			// Return no exception
			return null;
		}

		public int Count => _names.Length;
		public Name this[int index] => _names[index];

		public string RegularExpression { get; }

		private readonly ImmutableArray<Name> _names;
		private readonly IEnumerable<Name> _namesEnumerable;

		public Names(IEnumerable<Name> names) : this(names?.ToImmutableArray() ?? ImmutableArray<Name>.Empty) { }
		public Names(ImmutableArray<Name> names)
		{
			// Throw error if exception exists
			_GetException(names).ThrowIfExists();
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

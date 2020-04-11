using BoningerWorks.TextAdventure.Engine.Static;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Utilities
{
	public class Names : IReadOnlyList<Name>
	{
		public int Count => _names.Count;
		public Name this[int index] => _names[index];

		public string RegularExpression { get; }

		private readonly ImmutableList<Name> _names;

		public Names(IEnumerable<string> strings) : this(strings.Select(s => new Name(s))) { }
		public Names(IEnumerable<Name> names) : this(names.ToImmutableList()) { }
		public Names(ImmutableList<Name> names)
		{
			// Set names
			_names = names;
			// Set regular expression
			RegularExpression = _CreateRegularExpression(_names);
		}

		private static string _CreateRegularExpression(ImmutableList<Name> names)
		{
			// Create regular expression
			var regularExpression = string.Join(@"|", names.Select(n => RegularExpressions.CreateNonCapturingGroup(n.RegularExpression)));
			// Return regular expression
			return regularExpression;
		}

		public IEnumerator<Name> GetEnumerator() => _names.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _names.GetEnumerator();
	}
}

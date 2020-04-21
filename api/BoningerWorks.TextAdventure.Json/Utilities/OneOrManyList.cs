using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Json.Utilities
{
	public class OneOrManyList<TValue> : List<TValue>
	{
		public OneOrManyList() { }
		public OneOrManyList(IEnumerable<TValue> collection) : base(collection) { }
		public OneOrManyList(int capacity) : base(capacity) { }
	}
}

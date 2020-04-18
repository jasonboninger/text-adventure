using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Exceptions.Data
{
	public class AmbiguousItemMatchData
	{
		public Name Name { get; }
		public ImmutableArray<Item> Items { get; }

		public AmbiguousItemMatchData(Name name, ImmutableArray<Item> items)
		{
			// Set name
			Name = name;
			// Set items
			Items = items;
		}
	}
}

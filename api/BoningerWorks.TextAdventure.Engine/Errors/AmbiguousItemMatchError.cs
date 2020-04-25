using BoningerWorks.TextAdventure.Core.Interfaces;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Executables;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Errors
{
	public class AmbiguousItemMatchError : IError
	{
		public string Message => $"Name ({Name}) matches more than one item.";

		public Name Name { get; }
		public ImmutableArray<Item> Items { get; }

		public AmbiguousItemMatchError(Name name, ImmutableArray<Item> items)
		{
			// Set name
			Name = name;
			// Set items
			Items = items;
		}
	}
}

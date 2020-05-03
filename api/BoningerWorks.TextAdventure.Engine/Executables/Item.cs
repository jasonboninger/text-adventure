using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Item : IEntity
	{
		private static readonly Symbol _datumId = new Symbol("ID");
		private static readonly Symbol _datumName = new Symbol("NAME");

		public Symbol Symbol { get; }
		public Names Names { get; }
		public ImmutableDictionary<Symbol, string> Metadata { get; }

		public Item(ItemMap itemMap)
		{
			// Set symbol
			Symbol = itemMap.ItemSymbol;
			// Set names
			Names = itemMap.ItemNames;
			// Create metadata
			Metadata = ImmutableDictionary.CreateRange(new KeyValuePair<Symbol, string>[]
			{
				KeyValuePair.Create(_datumId, Symbol.ToString()),
				KeyValuePair.Create(_datumName, Names.Name.ToString())
			});
		}

		public override string ToString()
		{
			// Return string
			return Symbol.ToString();
		}
	}
}

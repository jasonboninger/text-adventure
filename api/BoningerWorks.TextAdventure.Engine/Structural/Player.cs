using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Structural
{
	public class Player : IEntity
	{
		public Id Id { get; }
		public Names Names { get; }
		public ImmutableDictionary<Id, string> Metadata { get; }

		public Player(PlayerMap playerMap)
		{
			// Set ID
			Id = playerMap.PlayerId;
			// Set names
			Names = playerMap.PlayerNames;
			// Create metadata
			Metadata = ImmutableDictionary.CreateRange(new KeyValuePair<Id, string>[]
			{
				KeyValuePair.Create(Static.Metadata.Id, Id.ToString()),
				KeyValuePair.Create(Static.Metadata.Name, Names.Name.ToString())
			});
		}

		public override string ToString()
		{
			// Return string
			return Id.ToString();
		}
	}
}

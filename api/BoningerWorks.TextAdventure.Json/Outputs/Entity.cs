using BoningerWorks.TextAdventure.Core.Utilities;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Json.Outputs
{
	public class Entity
	{
		public ImmutableDictionary<Id, string> Data { get; }

		public Entity(ImmutableDictionary<Id, string>? data = null)
		{
			// Set data
			Data = data ?? ImmutableDictionary<Id, string>.Empty;
		}

		public Entity UpdateData(Id id, string value)
		{
			// Set data
			var data = Data.SetItem(id, value);
			// Create entity
			var entity = new Entity(data);
			// Return entity
			return entity;
		}
	}
}

using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.States.Errors;

namespace BoningerWorks.TextAdventure.Json.States.Data
{
	public class ItemData
	{
		public Symbol Location { get; set; }
		public bool Active { get; set; }

		public ItemData(Symbol location, bool active)
		{
			// Set location
			Location = location ?? throw GenericException.Create(new StateInvalidError("Location cannot be null."));
			// Set active
			Active = active;
		}
	}
}

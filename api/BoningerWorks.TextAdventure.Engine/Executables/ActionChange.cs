using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionChange : IAction<Message>
	{


		public ActionChange(Player player, Areas areas, Items items, ChangeMap changeMap)
		{

		}

		public IEnumerable<Message> Execute(State gameState)
		{




			// Return no message states
			yield break;
		}
	}
}

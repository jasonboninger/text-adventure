using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionChange : IAction<MessageState>
	{


		public ActionChange(Player player, Areas areas, Items items, ChangeMap changeMap)
		{

		}

		public IEnumerable<MessageState> Execute(GameState gameState)
		{




			// Return no message states
			yield break;
		}
	}
}

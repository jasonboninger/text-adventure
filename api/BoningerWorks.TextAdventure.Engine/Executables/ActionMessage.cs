using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionMessage : IAction<MessageState>
	{
		private readonly ImmutableArray<ActionLine> _actionsLine;

		public ActionMessage(MessageMap messageMap)
		{
			// Set line actions
			_actionsLine = messageMap.LineMaps.Select(lm => new ActionLine(lm)).ToImmutableArray();
		}

		public IEnumerable<MessageState> Execute(GameState gameState)
		{
			// Create line states
			var lineStates = _actionsLine.SelectMany(al => al.Execute(gameState)).ToImmutableList();
			// Create message state
			var messageState = new MessageState(lineStates);
			// Return message state
			yield return messageState;
		}
	}
}

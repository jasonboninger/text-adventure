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
			var lineStates = ImmutableList.CreateBuilder<LineState>();
			// Run through line actions
			for (int i = 0; i < _actionsLine.Length; i++)
			{
				// Add line states
				lineStates.AddRange(_actionsLine[i].Execute(gameState));
			}
			// Create message state
			var messageState = new MessageState(lineStates.ToImmutable());
			// Return message state
			yield return messageState;
		}
	}
}

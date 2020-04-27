using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionLineInlined : IAction<LineState>
	{
		private readonly ImmutableArray<ActionText> _actionsText;

		public ActionLineInlined(LineInlinedMap lineInlinedMap)
		{
			// Set text actions
			_actionsText = lineInlinedMap.TextMaps.Select(tm => new ActionText(tm)).ToImmutableArray();
		}

		public IEnumerable<LineState> Execute(GameState gameState)
		{
			// Create text states
			var textStates = _actionsText.SelectMany(at => at.Execute(gameState)).ToImmutableList();
			// Create line content state
			var lineContentState = new LineContentState(textStates);
			// Create line state
			var lineState = new LineState(lineContentState);
			// Return line state
			yield return lineState;
		}
	}
}

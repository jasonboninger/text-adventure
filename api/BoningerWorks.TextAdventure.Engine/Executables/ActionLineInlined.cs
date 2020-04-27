using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionLineInlined : IAction<Line>
	{
		private readonly ImmutableArray<ActionText> _actionsText;

		public ActionLineInlined(LineInlinedMap lineInlinedMap)
		{
			// Set text actions
			_actionsText = lineInlinedMap.TextMaps.Select(tm => new ActionText(tm)).ToImmutableArray();
		}

		public IEnumerable<Line> Execute(State gameState)
		{
			// Create text states
			var textStates = _actionsText.SelectMany(at => at.Execute(gameState)).ToImmutableList();
			// Create line content state
			var lineContentState = new LineContent(textStates);
			// Create line state
			var lineState = new Line(lineContentState);
			// Return line state
			yield return lineState;
		}
	}
}

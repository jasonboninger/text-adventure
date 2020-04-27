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
			var textStates = ImmutableList.CreateBuilder<TextState>();
			// Run through text actions
			for (int i = 0; i < _actionsText.Length; i++)
			{
				// Add text states
				textStates.AddRange(_actionsText[i].Execute(gameState));
			}
			// Create line content state
			var lineContentState = new LineContentState(textStates.ToImmutable());
			// Create line state
			var lineState = new LineState(lineContentState);
			// Return line state
			yield return lineState;
		}
	}
}

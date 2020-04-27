using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionTextInlined : IAction<TextState>
	{
		private readonly TextState _textState;

		public ActionTextInlined(TextInlinedMap textInlinedMap)
		{
			// Set text state
			_textState = new TextState(textInlinedMap.Value);
		}

		public IEnumerable<TextState> Execute(GameState gameState)
		{
			// Return text state
			yield return _textState;
		}
	}
}

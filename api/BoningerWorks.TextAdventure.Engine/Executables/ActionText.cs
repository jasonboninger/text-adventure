using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionText : IAction<TextState>
	{
		private readonly IAction<TextState> _actionText;

		public ActionText(TextMap textMap)
		{
			// Check if if map exists
			if (textMap.IfMap != null)
			{



				// Throw error
				throw new InvalidOperationException("Text map if is not implemented.");



			}
			// Check if inlined map exists
			if (textMap.InlinedMap != null)
			{
				// Set inlined text action
				_actionText = new ActionTextInlined(textMap.InlinedMap);
				// Return
				return;
			}
			// Throw error
			throw new InvalidOperationException("Text map could not be parsed.");
		}

		public IEnumerable<TextState> Execute(GameState gameState) => _actionText.Execute(gameState);
	}
}

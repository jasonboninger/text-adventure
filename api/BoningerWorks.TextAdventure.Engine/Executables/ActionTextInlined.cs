using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionTextInlined : IAction<Text>
	{
		private readonly Text _textState;

		public ActionTextInlined(TextInlinedMap textInlinedMap)
		{
			// Set text state
			_textState = new Text(textInlinedMap.Value);
		}

		public IEnumerable<Text> Execute(State gameState)
		{
			// Return text state
			yield return _textState;
		}
	}
}

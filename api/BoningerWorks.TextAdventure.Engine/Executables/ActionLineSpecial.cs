using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.States;
using BoningerWorks.TextAdventure.Json.States.Enums;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionLineSpecial : IAction<LineState>
	{
		private static readonly LineState _lineStateBlank = new LineState(new LineSpecialState(ELineSpecialType.Blank));
		private static readonly LineState _lineStateHorizontalRule = new LineState(new LineSpecialState(ELineSpecialType.HorizontalRule));

		private readonly LineState _lineState;

		public ActionLineSpecial(LineSpecialMap lineSpecialMap)
		{
			// Set line state
			_lineState = lineSpecialMap.Type switch
			{
				ELineSpecialType.Blank => _lineStateBlank,
				ELineSpecialType.HorizontalRule => _lineStateHorizontalRule,
				_ => throw new ValidationError($"Special line type ({lineSpecialMap.Type}) could not be handled.")
			};
		}

		public IEnumerable<LineState> Execute(GameState gameState)
		{
			// Return line state
			yield return _lineState;
		}
	}
}

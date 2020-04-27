using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionLineSpecial : IAction<Line>
	{
		private static readonly Line _lineStateBlank = new Line(new LineSpecial(ELineSpecialType.Blank));
		private static readonly Line _lineStateHorizontalRule = new Line(new LineSpecial(ELineSpecialType.HorizontalRule));

		private readonly Line _lineState;

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

		public IEnumerable<Line> Execute(State gameState)
		{
			// Return line state
			yield return _lineState;
		}
	}
}

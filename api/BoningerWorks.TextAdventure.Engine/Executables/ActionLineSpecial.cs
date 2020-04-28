using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionLineSpecial
	{
		private static readonly Line _lineSpecialBlank = new Line(new LineSpecial(ELineSpecialType.Blank));
		private static readonly Func<State, Line> _actionLineSpecialBlank = state => _lineSpecialBlank;

		private static readonly Line _lineSpecialHorizontalRule = new Line(new LineSpecial(ELineSpecialType.HorizontalRule));
		private static readonly Func<State, Line> _actionLineSpecialHorizontalRule = state => _lineSpecialHorizontalRule;

		public static Func<State, Line> Create(LineSpecialMap lineSpecialMap)
		{
			// Return action
			return lineSpecialMap.Type switch
			{
				ELineSpecialType.Blank => _actionLineSpecialBlank,
				ELineSpecialType.HorizontalRule => _actionLineSpecialHorizontalRule,
				_ => throw new ValidationError($"Special line type ({lineSpecialMap.Type}) could not be handled.")
			};
		}
	}
}

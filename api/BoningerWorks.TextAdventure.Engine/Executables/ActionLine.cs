using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ActionLine : IAction<Line>
	{
		private readonly IAction<Line> _actionLine;

		public ActionLine(LineMap lineMap)
		{
			// Check if if map exists
			if (lineMap.IfMap != null)
			{



				// Throw error
				throw new InvalidOperationException("Line map if is not implemented.");



			}
			// Check if special map exists
			if (lineMap.SpecialMap != null)
			{
				// Set special line action
				_actionLine = new ActionLineSpecial(lineMap.SpecialMap);
				// Return
				return;
			}
			// Check if inlined map exists
			if (lineMap.InlinedMap != null)
			{
				// Set inlined line action
				_actionLine = new ActionLineInlined(lineMap.InlinedMap);
				// Return
				return;
			}
			// Throw error
			throw new InvalidOperationException("Line map could not be parsed.");
		}

		public IEnumerable<Line> Execute(State gameState) => _actionLine.Execute(gameState);
	}
}

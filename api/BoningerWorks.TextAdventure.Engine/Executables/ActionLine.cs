using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public static class ActionLine
	{
		public static Func<State, IEnumerable<Line>> Create(LineMap lineMap)
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
				// Return special line execute
				return new ActionLineSpecial(lineMap.SpecialMap).Execute;
			}
			// Check if inlined map exists
			if (lineMap.InlinedMap != null)
			{
				// Return inlined line execute
				return new ActionLineInlined(lineMap.InlinedMap).Execute;
			}
			// Throw error
			throw new InvalidOperationException("Line map could not be parsed.");
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Transient;
using BoningerWorks.TextAdventure.Intermediate.Enums;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using System;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionSpecial
	{
		public static Action<ResultBuilder> Create(EActionSpecial actionSpecial)
		{
			// Return action
			return actionSpecial switch
			{
				EActionSpecial.End => r =>
				{
					// Update state
					var state = r.State.UpdateComplete(true);
					// Set state
					r.State = state;
				},
				_ => throw new ValidationError($"Special action ({actionSpecial}) could not be handled.")
			};
		}
	}
}

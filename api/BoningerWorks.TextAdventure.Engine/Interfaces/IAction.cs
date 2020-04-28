using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Interfaces
{
	public interface IAction<out TValue>
	where TValue : class
	{
		IEnumerable<TValue> Execute(State state);
	}
}

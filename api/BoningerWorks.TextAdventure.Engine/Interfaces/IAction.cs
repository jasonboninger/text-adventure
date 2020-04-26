using BoningerWorks.TextAdventure.Json.States;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Interfaces
{
	public interface IAction<out TValue>
	where TValue : class
	{
		IEnumerable<TValue> Execute(GameState gameState);
	}
}

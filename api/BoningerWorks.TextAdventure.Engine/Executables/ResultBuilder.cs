using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class ResultBuilder
	{
		public State State { get; set; }

		public ImmutableList<Message>.Builder Messages { get; } = ImmutableList.CreateBuilder<Message>();

		public ResultBuilder(State state)
		{
			// Set state
			State = state;
		}

		public Result ToImmutable()
		{
			// Create result
			var result = new Result(State, Messages.ToImmutable());
			// Return result
			return result;
		}
	}
}

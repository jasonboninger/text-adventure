using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Json.Outputs;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Engine.Transient
{
	public class ResultBuilder
	{
		public State State { get; set; }

		public Game Game { get; }
		public ImmutableList<Message>.Builder Messages { get; }

		public ResultBuilder(Game game, State state)
		{
			// Set game
			Game = game;
			// Set messages
			Messages = ImmutableList.CreateBuilder<Message>();
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

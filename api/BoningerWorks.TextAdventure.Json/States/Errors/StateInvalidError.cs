using BoningerWorks.TextAdventure.Core.Interfaces;

namespace BoningerWorks.TextAdventure.Json.States.Errors
{
	public class StateInvalidError : IError
	{
		public string Message { get; }

		public StateInvalidError(string? message)
		{
			// Set message
			Message = "Game state is not valid." + (string.IsNullOrWhiteSpace(message) ? string.Empty : " " + message);
		}
	}
}

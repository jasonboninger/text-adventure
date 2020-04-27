using BoningerWorks.TextAdventure.Core.Interfaces;

namespace BoningerWorks.TextAdventure.Json.Outputs.Errors
{
	public class InvalidDataError : IError
	{
		public string Message { get; }

		public InvalidDataError(string? message)
		{
			// Set message
			Message = "Some game data is not valid." + (string.IsNullOrWhiteSpace(message) ? string.Empty : " " + message);
		}
	}
}

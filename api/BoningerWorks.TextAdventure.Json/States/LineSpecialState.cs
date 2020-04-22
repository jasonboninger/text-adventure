namespace BoningerWorks.TextAdventure.Json.States
{
	public class LineSpecialState
	{
		public static LineSpecialState Create(string type)
		{
			// Create special line state
			var lineSpecialState = new LineSpecialState
			{
				Type = type
			};
			// Return special line state
			return lineSpecialState;
		}

		public string Type { get; set; }
	}
}

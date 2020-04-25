using BoningerWorks.TextAdventure.Maps.Models;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		static void Main()
		{
			// Create game map
			var gameMap = GameMap.Deserialize(CrazyEx.JSON);
			//// Create game
			//var game = new Game(blueprint);
			//// Create state
			//var state = game.New();
			//// Loop forever
			//while (true)
			//{
			//	// Write line
			//	Console.WriteLine();
			//	// Ask for input
			//	Console.WriteLine("Give me some input...");
			//	// Get input
			//	var input = Console.ReadLine();
			//	// Execute input
			//	var messages = game.Execute(state, input);
			//	// Create first
			//	var first = true;
			//	// Run through messages
			//	foreach (var message in messages)
			//	{
			//		// Check if not first
			//		if (!first)
			//		{
			//			// Write ellipsis
			//			Console.WriteLine("...");
			//			// Wait for user
			//			Console.ReadKey();
			//		}
			//		// Set not first
			//		first = false;
			//		// Run through lines
			//		foreach (var line in message.Lines)
			//		{
			//			// Check if content
			//			if (line.Content != null)
			//			{
			//				// Get content
			//				var content = line.Content;
			//				// Write text
			//				Console.WriteLine(content.Text);
			//				// Continue
			//				continue;
			//			}
			//			// Check if special
			//			if (line.Special != null)
			//			{
			//				// Get special
			//				var special = line.Special;
			//				// Check type
			//				switch (special.Type)
			//				{
			//					case ELineSpecialType.Blank:
			//						// Write empty
			//						Console.WriteLine();
			//						break;
			//					case ELineSpecialType.HorizontalRule:
			//						// Write horizontal rule
			//						Console.WriteLine("====================================");
			//						break;
			//					default: throw new InvalidOperationException($"Special line type ({special.Type}) could not be handled.");
			//				}
			//				// Continue
			//				continue;
			//			}
			//		}
			//	}
			//}
		}
	}
}

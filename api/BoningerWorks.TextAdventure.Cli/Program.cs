using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Engine.Generated;
using BoningerWorks.TextAdventure.Engine.Static;
using System;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		static void Main()
		{
			// Create game blueprint
			var gameBlueprint = JsonSerializer.Deserialize<GameBlueprint>(CrazyEx.JSON, JsonConfigurations.CreateOptions());
			// Create game
			var game = new Game(gameBlueprint);
			// Loop forever
			while (true)
			{
				// Ask for input
				Console.WriteLine("Give me some input...");
				// Get input
				var input = Console.ReadLine();
				// Execute input
				var responses = game.Execute(input);
				// Run through responses
				foreach (var response in responses)
				{
					// Write response
					Console.WriteLine(response);
				}
			}
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Engine.Generated;
using BoningerWorks.TextAdventure.Engine.Json.Static;
using System;
using System.Text.Json;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		static void Main()
		{
			// Create blueprint
			var blueprint = JsonSerializer.Deserialize<GameBlueprint>(CrazyEx.JSON, JsonConfigurations.CreateOptions());
			// Create game
			var game = new Game(blueprint);
			// Create state
			var state = game.New();
			// Loop forever
			while (true)
			{
				// Write line
				Console.WriteLine();
				// Ask for input
				Console.WriteLine("Give me some input...");
				// Get input
				var input = Console.ReadLine();
				// Execute input
				var responses = game.Execute(state, input);
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

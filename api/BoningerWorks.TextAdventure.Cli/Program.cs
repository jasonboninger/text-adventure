﻿using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Engine.Generated;
using BoningerWorks.TextAdventure.Engine.Json.Static;
using BoningerWorks.TextAdventure.Engine.Maps;
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
				var messages = game.Execute(state, input);
				// Create first
				var first = true;
				// Run through messages
				foreach (var message in messages)
				{
					// Check if not first
					if (!first)
					{
						// Write ellipsis
						Console.WriteLine("...");
						// Wait for user
						Console.ReadKey();
					}
					// Set not first
					first = false;
					// Run through lines
					foreach (var line in message.Lines)
					{
						// Check if content
						if (line.Content != null)
						{
							// Get content
							var content = line.Content;
							// Write text
							Console.WriteLine(content.Text);
							// Continue
							continue;
						}
						// Check if special
						if (line.Special != null)
						{
							// Get special
							var special = line.Special;
							// Check type
							switch (special.Type)
							{
								case ELineSpecialType.Blank:
									// Write empty
									Console.WriteLine();
									break;
								case ELineSpecialType.HorizontalRule:
									// Write horizontal rule
									Console.WriteLine("====================================");
									break;
								default: throw new InvalidOperationException($"Special line type ({special.Type}) could not be handled.");
							}
							// Continue
							continue;
						}
					}
				}
			}
		}
	}
}

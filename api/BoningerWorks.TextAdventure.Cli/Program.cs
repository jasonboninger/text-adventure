﻿using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;
using System;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		static void Main()
		{
			// Create game
			var game = Game.Deserialize(CrazyEx.JSON);
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
				var result = game.Execute(state, input);
				// Set state
				state = result.State;
				// Get messages
				var messages = result.Messages;
				// Run through messages
				for (int i = 0; i < messages.Count; i++)
				{
					var message = messages[i];
					// Check if not first
					if (i > 0)
					{
						// Write ellipsis
						Console.WriteLine("...");
						// Wait for user
						Console.ReadKey();
					}
					// Get lines
					var lines = message.Lines;
					// Run through lines
					for (int k = 0; k < lines.Count; k++)
					{
						var line = lines[k];
						// Check if content
						if (line.Content != null)
						{
							// Get content
							var content = line.Content;
							// Get texts
							var texts = content.Texts;
							// Run through texts
							for (int m = 0; m < texts.Count; m++)
							{
								var text = texts[m];
								// Write text
								Console.Write(text.Value);
							}
							// Write line
							Console.WriteLine();
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
						// Throw error
						throw new InvalidOperationException("Line could not be displayed.");
					}
				}
			}
		}
	}
}

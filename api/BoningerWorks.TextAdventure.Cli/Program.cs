using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Json.Outputs;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;
using System;
using System.Collections.Immutable;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		private enum EColor
		{
			Input,
			Normal
		}

		static void Main()
		{
			// Set color
			_SetColor(EColor.Normal);
			// Create game
			var game = Game.Deserialize(CrazyEx.JSON);
			// Start game
			var result = game.New();
			// Set state
			var state = result.State;
			// Display messages
			_DisplayMessages(result.Messages);
			// Run game
			while (!state.Complete)
			{
				// Write line
				Console.WriteLine();
				// Write line
				Console.WriteLine();
				// Set color
				_SetColor(EColor.Input);
				// Display prompt messages
				_DisplayMessages(state.Prompt);
				// Create input
				string input;
				// Get input
				while (true)
				{
					// Get input
					input = Console.ReadLine();
					// Check if input does not exist
					if (string.IsNullOrWhiteSpace(input))
					{
						// Remove line
						Console.CursorTop--;
						// Continue
						continue;
					}
					// Stop
					break;
				}
				// Set color
				_SetColor(EColor.Normal);
				// Execute input
				result = game.Execute(state, input);
				// Set state
				state = result.State;
				// Get messages
				var messages = result.Messages;
				// Check if messages exist
				if (messages.Count > 0)
				{
					// Write line
					Console.WriteLine();
					// Write line
					Console.WriteLine();
				}
				// Display messages
				_DisplayMessages(messages);
			}
			// Loop forever
			while (true)
			{
				// Prevent input
				Console.ReadKey(intercept: true);
			}
		}

		private static void _DisplayMessages(ImmutableList<Message> messages)
		{
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
								Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '='));
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

		private static void _SetColor(EColor color)
		{
			// Check if color
			switch (color)
			{
				case EColor.Input:
					// Set color
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case EColor.Normal:
					// Set color
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				default:
					// Throw error
					throw new InvalidOperationException($"Color ({color}) could not be handled.");
			}
		}
	}
}

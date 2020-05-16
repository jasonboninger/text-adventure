using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Outputs;
using BoningerWorks.TextAdventure.Json.Outputs.Enums;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		private enum EColor
		{
			Input,
			Normal,
			Error
		}

		static void Main(string[] arguments)
		{
			// Clear console
			Console.Clear();
			// Set color
			_SetColor(EColor.Normal);
			// Create game
			Game game;
			// Try to load game
			try
			{
				// Load game
				game = _LoadGame(arguments);
			}
			catch (GenericException<ValidationError> exception)
			{
				// Check if validation error and message exists
				if (exception.Error != null && !string.IsNullOrWhiteSpace(exception.Error.Message))
				{
					// Display error
					_DisplayError(exception.Error.Message);
					// Return
					return;
				}
				// Throw error
				throw;
			}
			// Get development
			var development = (arguments?.Contains("--development", StringComparer.OrdinalIgnoreCase) ?? false)
				|| (arguments?.Contains("-d", StringComparer.OrdinalIgnoreCase) ?? false);
			// Run game
			_RunGame(game, development);
		}

		private static Game _LoadGame(string?[]? arguments)
		{
			// Get path
			var path = arguments != null && arguments.Length > 0 ? arguments[0] : null;
			// Check if path does not exist
			if (string.IsNullOrWhiteSpace(path))
			{
				// Throw error
				throw new ValidationError("File path cannot be null, empty, or whitespace.");
			}
			// Get folder
			var folder = Environment.CurrentDirectory;
			// Get file
			var file = Path.Combine(folder, path, "game.json");
			// Check if file does not exist
			if (!File.Exists(file))
			{
				// Throw error
				throw new ValidationError($"File ({file}) could not be found.");
			}
			// Get JSON
			var json = File.ReadAllText(file);
			// Create game
			var game = Game.Deserialize(json);
			// Return game
			return game;
		}

		private static void _RunGame(Game game, bool development)
		{
			// Get commands
			var commands = (development ? game.Development.TestCommands : ImmutableArray<string>.Empty).ToList();
			// Set color
			_SetColor(EColor.Normal);
			// Start game
			var result = game.New();
			// Set state
			var state = result.State;
			// Set wait
			var wait = commands.Count == 0;
			// Display effect messages
			_DisplayMessages(result.MessagesEffect, wait);
			// Run game
			while (!state.Complete)
			{
				// Set wait
				wait = commands.Count == 0;
				// Write line
				Console.WriteLine();
				// Write line
				Console.WriteLine();
				// Set color
				_SetColor(EColor.Input);
				// Display prompt messages
				_DisplayMessages(result.MessagesPrompt, wait);
				// Create input
				string input;
				// Get input
				while (true)
				{
					// Check if commands exist
					if (commands.Count > 0)
					{
						// Set input
						input = commands[0];
						// Remove command
						commands.RemoveAt(0);
						// Write input
						Console.WriteLine(input);
					}
					else
					{
						// Get input
						input = Console.ReadLine();
					}
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
				// Check if effect messages exist
				if (result.MessagesEffect.Count > 0)
				{
					// Write line
					Console.WriteLine();
					// Write line
					Console.WriteLine();
				}
				// Display effect messages
				_DisplayMessages(result.MessagesEffect, wait);
			}
		}

		private static void _DisplayMessages(ImmutableList<Message> messages, bool wait)
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
					// Check if wait
					if (wait)
					{
						// Wait for user
						Console.ReadKey(intercept: true);
					}
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

		private static void _DisplayError(string message)
		{
			// Set color
			_SetColor(EColor.Error);
			// Preface error
			Console.WriteLine("Game JSON could not be parsed. Please fix the following issue:");
			// Write line
			Console.WriteLine();
			// Show error
			Console.WriteLine(message);
			// Set color
			_SetColor(EColor.Normal);
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
				case EColor.Error:
					// Set color
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.DarkRed;
					break;
				default:
					// Throw error
					throw new InvalidOperationException($"Color ({color}) could not be handled.");
			}
		}
	}
}

using BoningerWorks.TextAdventure.Engine.Executables;
using BoningerWorks.TextAdventure.Engine.Generated;
using BoningerWorks.TextAdventure.Engine.Blueprints;
using BoningerWorks.TextAdventure.Engine.Static;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Linq;
using System.Text.Json;
using Command = BoningerWorks.TextAdventure.Engine.Executables.Command;

namespace BoningerWorks.TextAdventure.Cli
{
	static class Program
	{
		static void Main()
		{
			var game = JsonSerializer.Deserialize<GameBlueprint>(CrazyEx.JSON, JsonConfigurations.CreateOptions());

			var items = new Items(game.Player.Items.Select(kv => new Item(new Symbol(kv.Key), kv.Value)));
			var commands = game.Templates.Commands.Select(kv => new Command(new Symbol(kv.Key), new CommandParser(items, kv.Value))).ToList();

			Console.WriteLine(game);
			Console.WriteLine(game.Templates.Commands.Count);

			while (true)
			{
				Console.WriteLine("Give me some input...");

				var input = Console.ReadLine();

				for (int i = 0; i < commands.Count; i++)
				{
					var command = commands[i];
					if (command.Parser.TryParseInput(input, out var itemSymbolToItemMappings))
					{
						Console.WriteLine($"The {command} command was triggered!");
						if (itemSymbolToItemMappings.Count == 0)
						{
							Console.WriteLine("No item symbols were returned.");
						}
						else
						{
							foreach (var keyValue in itemSymbolToItemMappings)
							{
								Console.WriteLine($"The {keyValue.Key} item symbol returned the {keyValue.Value.Single()} item.");
							}
						}
					}
				}
			}
		}
	}
}

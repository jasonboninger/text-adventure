using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Command
	{
		public Symbol Symbol { get; }
		public ImmutableArray<Symbol> Parts { get; }
		public ImmutableDictionary<Symbol, Names> WordToNamesMappings { get; }
		public ImmutableArray<Symbol> CommandItems { get; }

		private readonly Items _items;
		private readonly Regex _regularExpression;

		public Command(Items items, CommandMap commandMap)
		{
			// Set items
			_items = items;
			// Set symbol
			Symbol = commandMap.CommandSymbol;
			// Set parts
			Parts = commandMap.PartSymbols;
			// Set word to names mappings
			WordToNamesMappings = commandMap.WordSymbolToWordNamesMappings;
			// Set command items
			CommandItems = commandMap.CommandItemSymbols;
			// Set regular expression
			_regularExpression = _CreateRegularExpression();
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
		}

		public ImmutableDictionary<Symbol, Item>? TryGetMatch(string? input)
		{
			// Create command item to item mappings
			ImmutableDictionary<Symbol, Item>? commandItemToItemMappings = null;
			// Check if input exists
			if (input != null)
			{
				// Get match
				var match = _regularExpression.Match(input);
				// Check if match succeeded
				if (match.Success)
				{
					// Create command item to item mappings builder
					var commandItemToItemMappingsBuilder = ImmutableDictionary.CreateBuilder<Symbol, Item>();
					// Run through command items
					foreach (var commandItem in CommandItems)
					{
						// Get command item group
						var commandItemGroup = commandItem.ToString();
						// Get group
						var group = match.Groups[commandItemGroup];
						// Get item name
						var itemName = new Name(group.Value);
						// Get item
						var item = _items.Get(itemName);
						// Add command item to item mapping
						commandItemToItemMappingsBuilder.Add(commandItem, item);
					}
					// Set command item to item mappings
					commandItemToItemMappings = commandItemToItemMappingsBuilder.ToImmutable();
				}
			}
			// Return command item to item mappings
			return commandItemToItemMappings;
		}

		private Regex _CreateRegularExpression()
		{
			// Create regular expressions
			var regularExpressions = Parts.Select(p =>
			{
				// Check if word exists
				if (WordToNamesMappings.TryGetValue(p, out var names))
				{
					// Create word regular expression
					var regularExpressionWord = RegularExpressions.CreateNonCapturingGroup(names.RegularExpression);
					// Return word regular expression
					return regularExpressionWord;
				}
				// Check if command item exists
				if (CommandItems.Contains(p))
				{
					// Create command item regular expression
					var regularExpressionCommandItem = RegularExpressions.CreateCaptureGroup(p.ToString(), _items.RegularExpression);
					// Return command item regular expression
					return regularExpressionCommandItem;
				}
				// Throw error
				throw new ValidationError($"Command ({Symbol}) part ({p}) could not be found in words or command items.");
			});
			// Create regular expression options
			var regularExpressionOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
			// Create regular expression
			var regularExpression = new Regex(@"^" + string.Join(@" +", regularExpressions) + @"$", regularExpressionOptions);
			// Return regular expression
			return regularExpression;
		}
	}
}

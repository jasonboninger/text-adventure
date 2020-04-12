using BoningerWorks.TextAdventure.Engine.Blueprints.Templates;
using BoningerWorks.TextAdventure.Engine.Static;
using BoningerWorks.TextAdventure.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class CommandParser
	{
		private readonly Items _items;
		private readonly Regex _regularExpression;
		private readonly ImmutableArray<Symbol> _itemSymbols;

		public CommandParser(Items items, CommandTemplate commandTemplate)
		{
			// Set items
			_items = items ?? throw new ArgumentException("Items cannot be null.", nameof(items));
			// Check if command template does not exist
			if (commandTemplate == null)
			{
				// Throw error
				throw new ArgumentException("Command template cannot be null.", nameof(commandTemplate));
			}
			// Get part symbols
			var partSymbols = _CreatePartSymbols(commandTemplate.Parts);
			// Check if not all part symbols are unique
			if (partSymbols.Distinct().Count() != partSymbols.Count)
			{
				// Throw error
				throw new InvalidOperationException("Not all part symbols are unique.");
			}
			// Get word symbol to word names mappings
			var wordSymbolToWordNamesMappings = _CreateWordSymbolToWordNamesMappings(commandTemplate.Words);
			// Get item symbols
			var itemSymbols = _CreateItemSymbols(commandTemplate.Items);
			// Get word and item symbols
			var wordAndItemSymbols = Enumerable.Empty<Symbol>()
				.Concat(wordSymbolToWordNamesMappings.Keys)
				.Concat(itemSymbols)
				.ToList();
			// Check if not all word and item symbols are unique
			if (wordAndItemSymbols.Distinct().Count() != wordAndItemSymbols.Count)
			{
				// Throw error
				throw new InvalidOperationException("Not all word and item symbols are unique.");
			}
			// Set regular expression
			_regularExpression = _CreateRegularExpression(_items, partSymbols, wordSymbolToWordNamesMappings, itemSymbols);
			// Set item symbols
			_itemSymbols = partSymbols.Where(p => itemSymbols.Contains(p)).ToImmutableArray();
		}

		public bool TryParseInput(string input, out Dictionary<Symbol, ImmutableArray<Item>> itemSymbolToItemsMappings)
		{
			// Get match
			var match = _regularExpression.Match(input);
			// Check if match was not successful
			if (!match.Success)
			{
				// Set item symbol to items mappings
				itemSymbolToItemsMappings = null;
				// Return failed
				return false;
			}
			// Create item symbol to items mappings
			itemSymbolToItemsMappings = new Dictionary<Symbol, ImmutableArray<Item>>();
			// Run through item symbols
			for (int i = 0; i < _itemSymbols.Length; i++)
			{
				var itemSymbol = _itemSymbols[i];
				// Get item group
				var itemGroup = itemSymbol.ToString();
				// Get group
				var group = match.Groups[itemGroup];
				// Create item name
				var itemName = new Name(group.Value);
				// Get items
				var items = _items[itemName];
				// Add item symbol to items mapping
				itemSymbolToItemsMappings.Add(itemSymbol, items);
			}
			// Return succeeded
			return true;
		}

		private static List<Symbol> _CreatePartSymbols(List<string> parts)
		{
			// Check if parts does not exist
			if (parts == null)
			{
				// Throw error
				throw new ArgumentException("Parts cannot be null.", nameof(parts));
			}
			// Create part symbols
			var partSymbols = parts.Select(p => new Symbol(p)).ToList();
			// Return part symbols
			return partSymbols;
		}

		private static Dictionary<Symbol, Names> _CreateWordSymbolToWordNamesMappings(Dictionary<string, OneOrManyList<string>> words)
		{
			// Check if words does not exist
			if (words == null)
			{
				// Return no word symbol to word names mappings
				return new Dictionary<Symbol, Names>();
			}
			// Create word symbol to word names mappings
			var wordSymbolToWordNamesMappings = words.ToDictionary(kv => new Symbol(kv.Key), kv => new Names(kv.Value));
			// Return word symbol to word names mappings
			return wordSymbolToWordNamesMappings;
		}

		private static List<Symbol> _CreateItemSymbols(List<string> items)
		{
			// Check if items does not exist
			if (items == null)
			{
				// Return no item symbols
				return new List<Symbol>();
			}
			// Create item symbols
			var itemSymbols = items.Select(i => new Symbol(i)).ToList();
			// Return item symbols
			return itemSymbols;
		}

		private static Regex _CreateRegularExpression
		(
			Items items,
			List<Symbol> partSymbols, 
			Dictionary<Symbol, Names> wordSymbolToWordNamesMappings, 
			List<Symbol> itemSymbols
		)
		{
			// Create regular expressions
			var regularExpressions = partSymbols
				.Select
					(ps =>
					{
						// Check if word names exists
						if (wordSymbolToWordNamesMappings.TryGetValue(ps, out var wordNames))
						{
							// Create names regular expression
							var regularExpressionNames = RegularExpressions.CreateNonCapturingGroup(wordNames.RegularExpression);
							// Return names regular expression
							return regularExpressionNames;
						}
						// Check if item symbol exists
						if (itemSymbols.Contains(ps))
						{
							// Create item regular expression
							var regularExpressionItem = RegularExpressions.CreateCaptureGroup(ps.ToString(), items.RegularExpression);
							// Return item regular expression
							return regularExpressionItem;
						}
						// Throw error
						throw new InvalidOperationException($"Part symbol ({ps}) could not be found in words or items.");
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

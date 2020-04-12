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
		public ImmutableArray<Symbol> ItemSymbols { get; }

		private readonly Items _items;
		private readonly Regex _regularExpression;

		public CommandParser(Items items, CommandTemplate commandTemplate)
		{
			// Set items
			_items = items;
			// Check if command template does not exist
			if (commandTemplate == null)
			{
				// Throw error
				throw new ArgumentException("Command template cannot be null.", nameof(commandTemplate));
			}
			// Get part symbols
			var partSymbols = _CreatePartSymbols(commandTemplate.Parts);
			// Check if not all part symbols are unique
			if (partSymbols.Distinct().Count() != partSymbols.Length)
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
			ItemSymbols = partSymbols.Where(p => itemSymbols.Contains(p)).ToImmutableArray();
		}

		public bool TryMatchCommand(string input, out ImmutableDictionary<Symbol, ImmutableArray<Item>> itemSymbolToItemsMappings)
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
			// Create item symbol to items mappings builder
			var itemSymbolToItemsMappingsBuilder = ImmutableDictionary.CreateBuilder<Symbol, ImmutableArray<Item>>();
			// Run through item symbols
			for (int i = 0; i < ItemSymbols.Length; i++)
			{
				var itemSymbol = ItemSymbols[i];
				// Get item group
				var itemGroup = itemSymbol.ToString();
				// Get group
				var group = match.Groups[itemGroup];
				// Create item name
				var itemName = new Name(group.Value);
				// Get items
				var items = _items[itemName];
				// Add item symbol to items mapping
				itemSymbolToItemsMappingsBuilder.Add(itemSymbol, items);
			}
			// Set item symbol to items mappings
			itemSymbolToItemsMappings = itemSymbolToItemsMappingsBuilder.ToImmutable();
			// Return succeeded
			return true;
		}

		private static ImmutableArray<Symbol> _CreatePartSymbols(List<string> parts)
		{
			// Check if parts does not exist
			if (parts == null)
			{
				// Throw error
				throw new ArgumentException("Parts cannot be null.", nameof(parts));
			}
			// Create part symbols
			var partSymbols = parts.Select(p => new Symbol(p)).ToImmutableArray();
			// Return part symbols
			return partSymbols;
		}

		private static ImmutableDictionary<Symbol, Names> _CreateWordSymbolToWordNamesMappings(Dictionary<string, OneOrManyList<string>> words)
		{
			// Check if words does not exist
			if (words == null)
			{
				// Return no word symbol to word names mappings
				return ImmutableDictionary<Symbol, Names>.Empty;
			}
			// Create word symbol to word names mappings
			var wordSymbolToWordNamesMappings = words.ToImmutableDictionary(kv => new Symbol(kv.Key), kv => new Names(kv.Value));
			// Return word symbol to word names mappings
			return wordSymbolToWordNamesMappings;
		}

		private static ImmutableList<Symbol> _CreateItemSymbols(List<string> items)
		{
			// Check if items does not exist
			if (items == null)
			{
				// Return no item symbols
				return ImmutableList<Symbol>.Empty;
			}
			// Create item symbols
			var itemSymbols = items.Select(i => new Symbol(i)).ToImmutableList();
			// Return item symbols
			return itemSymbols;
		}

		private static Regex _CreateRegularExpression
		(
			Items items,
			ImmutableArray<Symbol> partSymbols,
			ImmutableDictionary<Symbol, Names> wordSymbolToWordNamesMappings,
			ImmutableList<Symbol> itemSymbols
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

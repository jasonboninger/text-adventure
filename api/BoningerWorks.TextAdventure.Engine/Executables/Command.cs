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
	public class Command
	{
		public Symbol Symbol { get; }
		public ImmutableArray<Symbol> PartSymbols { get; }
		public ImmutableArray<Tuple<Symbol, Names>> WordSymbols { get; }
		public ImmutableArray<Symbol> ItemSymbols { get; }

		private readonly Items _items;
		private readonly Regex _regularExpression;

		public Command(Symbol symbol, Items items, CommandTemplate commandTemplate)
		{
			// Set symbol
			Symbol = symbol;
			// Set items
			_items = items;
			// Check if command template does not exist
			if (commandTemplate == null)
			{
				// Throw error
				throw new ArgumentException($"Command ({Symbol}) template cannot be null.", nameof(commandTemplate));
			}
			// Get part symbols
			PartSymbols = _CreatePartSymbols(commandTemplate.Parts);
			// Check if not all part symbols are unique
			if (PartSymbols.Distinct().Count() != PartSymbols.Length)
			{
				// Throw error
				throw new InvalidOperationException($"Command ({Symbol}) has duplicate part symbols.");
			}
			// Get word symbol to word names mappings
			var wordSymbolToWordNamesMappings = _CreateWordSymbolToWordNamesMappings(commandTemplate.Words);
			// Get item symbols
			ItemSymbols = _CreateItemSymbols(commandTemplate.Items);
			// Get word and item symbols
			var wordAndItemSymbols = Enumerable.Empty<Symbol>()
				.Concat(wordSymbolToWordNamesMappings.Keys)
				.Concat(ItemSymbols)
				.ToList();
			// Check if not all word and item symbols are unique
			if (wordAndItemSymbols.Distinct().Count() != wordAndItemSymbols.Count)
			{
				// Throw error
				throw new InvalidOperationException($"Command ({Symbol}) has duplicate word and item symbols.");
			}
			// Check if number of part symbols is less than the number of word and item symbols
			if (PartSymbols.Length < wordAndItemSymbols.Count)
			{
				// Throw error
				throw new InvalidOperationException($"Command ({Symbol}) has word or item symbols which are not included in part symbols.");
			}
			// Set regular expression
			_regularExpression = _CreateRegularExpression(wordSymbolToWordNamesMappings);
			// Set word symbols
			WordSymbols = PartSymbols
				.Select(p => wordSymbolToWordNamesMappings.TryGetValue(p, out var n) ? Tuple.Create(p, n) : null)
				.Where(w => w != null)
				.ToImmutableArray();
		}

		public override string ToString()
		{
			// Return symbol
			return Symbol.ToString();
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

		private ImmutableArray<Symbol> _CreatePartSymbols(List<string> parts)
		{
			// Check if parts does not exist
			if (parts == null)
			{
				// Throw error
				throw new ArgumentException($"Command ({Symbol}) parts cannot be null.", nameof(parts));
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
			var wordSymbolToWordNamesMappings = words
				.ToImmutableDictionary
					(
						kv => new Symbol(kv.Key), 
						kv => new Names(kv.Value.Select(n => new Name(n)))
					);
			// Return word symbol to word names mappings
			return wordSymbolToWordNamesMappings;
		}

		private static ImmutableArray<Symbol> _CreateItemSymbols(List<string> items)
		{
			// Check if items does not exist
			if (items == null)
			{
				// Return no item symbols
				return ImmutableArray<Symbol>.Empty;
			}
			// Create item symbols
			var itemSymbols = items.Select(i => new Symbol(i)).ToImmutableArray();
			// Return item symbols
			return itemSymbols;
		}

		private Regex _CreateRegularExpression(ImmutableDictionary<Symbol, Names> wordSymbolToWordNamesMappings)
		{
			// Create regular expressions
			var regularExpressions = PartSymbols
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
						if (ItemSymbols.Contains(ps))
						{
							// Create item regular expression
							var regularExpressionItem = RegularExpressions.CreateCaptureGroup(ps.ToString(), _items.RegularExpression);
							// Return item regular expression
							return regularExpressionItem;
						}
						// Throw error
						throw new InvalidOperationException($"Command ({Symbol}) part symbol ({ps}) could not be found in words or items.");
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

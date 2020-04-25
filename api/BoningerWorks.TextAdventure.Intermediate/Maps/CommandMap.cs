using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Json.Inputs;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class CommandMap
	{
		public Symbol CommandSymbol { get; }
		public ImmutableArray<Symbol> PartSymbols { get; }
		public ImmutableDictionary<Symbol, Names> WordSymbolToWordNamesMappings { get; }
		public ImmutableArray<Symbol> CommandItemSymbols { get; }

		internal CommandMap(string commandSymbol, Command? command)
		{
			// Set command symbol
			CommandSymbol = Symbol.TryCreate(commandSymbol) ?? throw new ValidationError($"Command symbol ({commandSymbol}) is not valid.");
			// Try to create command map
			try
			{
				// Check if command does not exist
				if (command == null)
				{
					// Throw error
					throw new ValidationError("Command body cannot be null.");
				}
				// Check if part symbols is does not exist
				if (command.PartSymbols == null || command.PartSymbols.Count == 0)
				{
					// Throw error
					throw new ValidationError("Part symbols cannot be null or empty.");
				}
				// Set part symbols
				PartSymbols = command.PartSymbols
					.Select(ps => Symbol.TryCreate(ps) ?? throw new ValidationError($"Part symbol ({ps}) is not valid."))
					.ToImmutableArray();
				// Check if not all part symbols are unique
				if (PartSymbols.Distinct().Count() != PartSymbols.Length)
				{
					// Throw error
					throw new ValidationError("Not all part symbols are unique.");
				}
				// Set word symbol to word names mappings
				WordSymbolToWordNamesMappings = command.WordSymbolToWordNamesMappings?
					.Select
						(kv => new
						{
							WordSymbol = Symbol.TryCreate(kv.Key) ?? throw new ValidationError($"Word symbol ({kv.Key}) is not valid."),
							WordNames = Names.TryCreate
								(
									kv.Value.Select(n => Name.TryCreate(n) ?? throw new ValidationError($"Word name ({n}) is not valid."))
								)
								?? throw new ValidationError($"Names is not valid.")
						})
					.Where(_ => PartSymbols.Contains(_.WordSymbol))
					.ToImmutableDictionary(_ => _.WordSymbol, _ => _.WordNames)
					?? ImmutableDictionary<Symbol, Names>.Empty;
				// Set command item symbols
				CommandItemSymbols = command.CommandItemSymbols?
					.Select(cis => Symbol.TryCreate(cis) ?? throw new ValidationError($"Command item symbol ({cis}) is not valid."))
					.Where(cis => PartSymbols.Contains(cis))
					.ToImmutableArray()
					?? ImmutableArray<Symbol>.Empty;
				// Create input symbols
				var inputSymbols = Enumerable.Empty<Symbol>()
					.Concat(WordSymbolToWordNamesMappings.Select(kv => kv.Key))
					.Concat(CommandItemSymbols)
					.ToList();
				// Check if not all input symbols are unique
				if (inputSymbols.Distinct().Count() != inputSymbols.Count)
				{
					// Throw error
					throw new ValidationError("Not all input symbols are unique.");
				}
				// Check if number of part symbols does not equal number of input symbols
				if (PartSymbols.Length != inputSymbols.Count)
				{
					// Throw error
					throw new ValidationError("Not all part symbols are found in input symbols.");
				}
			}
			catch (GenericException<ValidationError> exception)
			{
				// Throw error
				throw new ValidationError($"Command ({CommandSymbol}) is not valid.").ToGenericException(exception);
			}
		}
	}
}

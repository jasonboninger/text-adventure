using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Inputs;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class OptionsMap
	{
		public ImmutableArray<char> IgnoredCharacters { get; }

		public OptionsMap(Options? options)
		{
			// Set ignored characters
			IgnoredCharacters = options?.IgnoredCharacters?
				.Select
					(ic =>
					{
						// Check if ignored character does not exist
						if (string.IsNullOrWhiteSpace(ic))
						{
							// Throw error
							throw new ValidationError("Ignored character cannot be null, empty, or whitespace.");
						}
						// Check if more than one character
						if (ic.Length > 1)
						{
							// Throw error
							throw new ValidationError($"Ignored character ({ic}) must be a single character.");
						}
						// Get ignored character
						var ignoredCharacter = ic[0];
						// Check if space
						if (ignoredCharacter == ' ')
						{
							// Throw error
							throw new ValidationError("Ignored character cannot be a space.");
						}
						// Return ignored character
						return ignoredCharacter;
					})
				.Distinct()
				.ToImmutableArray()
				?? ImmutableArray<char>.Empty;
		}
	}
}

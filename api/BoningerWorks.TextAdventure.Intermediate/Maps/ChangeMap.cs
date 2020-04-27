using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ChangeMap
	{
		private const char _SEPARATOR_BUILT_IN = '@';
		private const char _SEPARATOR_CUSTOM = '.';

		public Symbol TargetSymbol { get; }
		public Symbol TargetDatum { get; }
		public bool CustomDatum { get; }
		public string NewValue { get; }

		internal ChangeMap(string targetPath, string? newValue)
		{
			// Create built-in separator
			var separator = _SEPARATOR_BUILT_IN;
			// Check if target path contains custom separator
			if (targetPath.Contains(_SEPARATOR_CUSTOM))
			{
				// Set custom separator
				separator = _SEPARATOR_CUSTOM;
				// Set custom datum
				CustomDatum = true;
			}
			// Get target parts
			var targetParts = targetPath.Split(separator);
			// Check if not exactly two target parts
			if (targetParts.Length != 2)
			{
				var message = $"Change path must have exactly two parts, but instead has {targetParts.Length}.";
				// Throw error
				throw new ValidationError(message);
			}
			// Set target symbol
			TargetSymbol = Symbol.TryCreate(targetParts[0]) ?? throw new ValidationError($"Change target symbol ({targetParts[0]}) is not valid.");
			// Set target datum
			TargetDatum = Symbol.TryCreate(targetParts[1]) ?? throw new ValidationError($"Change target datum ({targetParts[1]}) is not valid.");
			// Set new value
			NewValue = newValue ?? throw new ValidationError("Value cannot be null.");
		}
	}
}

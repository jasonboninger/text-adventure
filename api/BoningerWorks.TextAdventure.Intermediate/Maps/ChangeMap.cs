using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ChangeMap
	{
		public Symbol TargetSymbol { get; }
		public Symbol TargetDatum { get; }
		public string NewValue { get; }

		internal ChangeMap(string targetPath, string? newValue)
		{
			// Get target parts
			var targetParts = targetPath.Split('.');
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
			TargetDatum = Symbol.TryCreate(targetParts[1]) ?? throw new ValidationError($"Change target datum ({targetParts[0]}) is not valid.");
			// Set new value
			NewValue = newValue ?? throw new ValidationError("Value cannot be null.");
		}
	}
}

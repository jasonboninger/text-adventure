using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;

namespace BoningerWorks.TextAdventure.Intermediate.Maps
{
	public class ChangeMap
	{
		public Path Path { get; }
		public string Value { get; }

		internal ChangeMap(string path, string? value)
		{
			// Set path
			Path = Path.TryCreate(path) ?? throw new ValidationError($"Change path ({path}) is not a valid path.");
			// Set value
			Value = value ?? throw new ValidationError("Change value cannot be null.");
		}
	}
}

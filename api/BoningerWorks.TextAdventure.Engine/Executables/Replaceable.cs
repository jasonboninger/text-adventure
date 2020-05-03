using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Replaceable
	{
		private const string _GROUP_PATH = "PATH";

		private readonly string _value;
		private readonly ImmutableArray<Action<State, StringBuilder>> _replaces;

		public Replaceable(Func<Symbol, Symbol> replacer, Entities entities, string value)
		{
			// Set value
			_value = value;
			// Set replaces
			_replaces = new Regex(@"\${" + RegularExpressions.CreateCaptureGroup(_GROUP_PATH, @"[^}]+") + @"}")
				.Matches(_value)
				.GroupBy(m => m.Value)
				.Select
					(g =>
					{
						// Get match
						var match = g.First();
						// Get value
						var value = match.Value;
						// Get capture
						var capture = match.Groups[_GROUP_PATH].Value;
						// Get path
						var path = Path.TryCreate(capture) ?? throw new ValidationError($"Replacement path ({value}) is not valid.");
						// Get target
						var target = path.Target;
						// Replace target
						target = replacer(target);
						// Get entity
						var entity = entities.TryGet(target) ?? throw new ValidationError($"Entity for symbol ({target}) could not be found.");
						// Get datum
						var datum = path.Datum;
						// Create replace
						Action<State, StringBuilder> replace;
						// Check if metadata
						if (path.Metadata)
						{
							// Check if datum does not exist
							if (!entity.Metadata.ContainsKey(datum))
							{
								// Throw error
								throw new ValidationError($"Entity ({entity.Symbol}) metadata ({target}) does not exist.");
							}
							// Set metadata
							var metadata = entity.Metadata;
							// Set replace
							replace = (state, stringBuilder) =>
							{
								// Replace value
								stringBuilder.Replace(value, metadata[datum].ToString());
							};
						}
						else
						{
							// Set replace
							replace = (state, stringBuilder) =>
							{
								// Get data
								var data = state.Entities[target].Data;
								// Replace value
								stringBuilder.Replace(value, data.TryGetValue(datum, out var replacement) ? replacement : string.Empty);
							};
						}
						// Return replace
						return replace;
					})
				.ToImmutableArray();
		}

		public string Replace(State state)
		{
			// Create string builder
			var stringBuilder = new StringBuilder(_value);
			// Run through replaces
			for (int i = 0; i < _replaces.Length; i++)
			{
				// Execute replace
				_replaces[i](state, stringBuilder);
			}
			// Return string
			return stringBuilder.ToString();
		}
	}
}

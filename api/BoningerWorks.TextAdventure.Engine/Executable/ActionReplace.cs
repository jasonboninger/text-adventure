using BoningerWorks.TextAdventure.Core.Static;
using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Structural;
using BoningerWorks.TextAdventure.Intermediate.Errors;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BoningerWorks.TextAdventure.Engine.Executable
{
	public static class ActionReplace
	{
		private const string _GROUP_PATH = "PATH";
		
		private static readonly Regex _regularExpression = new Regex(@"\${" + RegularExpressions.CreateCaptureGroup(_GROUP_PATH, @"[^}]*") + @"}");

		public static Func<State, string> Create(Func<Id, Id> replacer, Entities entities, string value)
		{
			// Create replaces
			var replaces = _regularExpression
				.Matches(value)
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
						var entity = entities.TryGet(target) ?? throw new ValidationError($"Entity for ID ({target}) could not be found.");
						// Get datum
						var datum = path.Datum;
						// Create replace
						Action<State, StringBuilder> replace;
						// Check if metadata
						if (path.Metadata)
						{
							// Get metadata
							var metadata = entity.Metadata;
							// Check if datum does not exist
							if (!metadata.ContainsKey(datum))
							{
								// Throw error
								throw new ValidationError($"Entity ({entity.Id}) metadata ({datum}) does not exist.");
							}
							// Set replace
							replace = (s, sb) => sb.Replace(value, metadata[datum]);
						}
						else
						{
							// Set replace
							replace = (s, sb) =>
							{
								// Get data
								var data = s.Entities[target].Data;
								// Replace value
								sb.Replace(value, data.TryGetValue(datum, out var replaced) ? replaced : string.Empty);
							};
						}
						// Return replace
						return replace;
					})
				.ToList();
			// Return action
			return s =>
			{
				// Create string builder
				var stringBuilder = new StringBuilder(value);
				// Run through replaces
				for (int i = 0; i < replaces.Count; i++)
				{
					// Execute replace
					replaces[i](s, stringBuilder);
				}
				// Return string
				return stringBuilder.ToString();
			};
		}
	}
}

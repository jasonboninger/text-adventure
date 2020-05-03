using System;

namespace BoningerWorks.TextAdventure.Core.Utilities
{
	public class Path
	{
		private const char _SEPARATOR_METADATA = '>';
		private const char _SEPARATOR_DATA = '.';

		public static Path? TryCreate(string? target, string? datum, bool metadata)
		{
			// Create try
			var @try = true;
			// Try to create path
			try
			{
				// Return path
				return new Path(target!, datum!, metadata);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no path
					return null;
				}
				// Throw error
				throw;
			}
		}
		public static Path? TryCreate(string? @string)
		{
			// Create try
			var @try = true;
			// Try to create path
			try
			{
				// Return path
				return new Path(@string!);
			}
			catch
			{
				// Check if try
				if (@try)
				{
					// Return no path
					return null;
				}
				// Throw error
				throw;
			}
		}

		public Symbol Target { get; }
		public Symbol Datum { get; }
		public bool Metadata { get; }

		internal Path(string target, string datum, bool metadata) : this(target + (metadata ? _SEPARATOR_METADATA : _SEPARATOR_DATA) + datum) { }
		internal Path(string path)
		{
			// Check if path does not exist
			if (string.IsNullOrWhiteSpace(path))
			{
				// Throw error
				throw new ArgumentException("Path cannot be null, empty, or whitespace.", nameof(path));
			}
			// Create data separator
			var separator = _SEPARATOR_DATA;
			// Check if path contains metadata separator
			if (path.Contains(_SEPARATOR_METADATA))
			{
				// Set metadata separator
				separator = _SEPARATOR_METADATA;
				// Set metadata
				Metadata = true;
			}
			// Get parts
			var parts = path.Split(separator);
			// Check if not exactly two parts
			if (parts.Length != 2)
			{
				// Throw error
				throw new ArgumentException($"Path ({path}) must have exactly two parts, but instead has {parts.Length}.", nameof(path));
			}
			// Set target
			Target = new Symbol(parts[0]);
			// Set datum
			Datum = new Symbol(parts[1]);
		}
	}
}

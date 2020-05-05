using BoningerWorks.TextAdventure.Intermediate.Errors;
using System;
using System.Collections.Generic;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Triggers
	{
		private class Path
		{
			public ReactionPath From { get; }
			public ReactionPath To { get; }

			public Path(ReactionPath from, ReactionPath to)
			{
				// Set from
				From = from ?? throw new ArgumentException("From cannot be null.", nameof(from));
				// Set to
				To = to ?? throw new ArgumentException("To cannot be null.", nameof(to));
			}
		}

		private readonly List<Path> _paths = new List<Path>();

		public void Add(ReactionPath from, ReactionPath to)
		{
			// Create path
			var path = new Path(from, to);
			// Add path
			_paths.Add(path);
		}

		public void Validate()
		{
			// Create all
			var all = new List<ReactionPath>();
			// Create processing
			var processing = new Stack<ReactionPath>();
			// Run through paths
			for (int i = 0; i < _paths.Count; i++)
			{
				var root = _paths[i];
				// Add root
				processing.Push(root.To);
				// Search leaves
				while (processing.TryPop(out var leaf))
				{
					// Check if all contains leaf
					if (all.Contains(leaf))
					{
						// Throw error
						throw new ValidationError($"Recursive trigger for path ({leaf}) was detected.");
					}
					// Add leaf to all
					all.Add(leaf);
					// Run through paths
					for (int k = 0; k < _paths.Count; k++)
					{
						var child = _paths[k];
						// Check if from matches leaf
						if (child.From == leaf)
						{
							// Add child
							processing.Push(child.To);
						}
					}
				}
				// Clear all
				all.Clear();
			}
		}
	}
}

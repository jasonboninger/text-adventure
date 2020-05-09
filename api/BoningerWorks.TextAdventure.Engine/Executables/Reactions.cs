using BoningerWorks.TextAdventure.Core.Utilities;
using BoningerWorks.TextAdventure.Engine.Comparers;
using BoningerWorks.TextAdventure.Engine.Interfaces;
using BoningerWorks.TextAdventure.Intermediate.Maps;
using BoningerWorks.TextAdventure.Json.Outputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BoningerWorks.TextAdventure.Engine.Executables
{
	public class Reactions : IReadOnlyList<Reaction>
	{
		public Reaction this[int index] => _reactions[index];
		public int Count => _reactions.Length;

		private readonly Entities _entities;
		private readonly Commands _commands;
		private readonly ImmutableArray<ReactionPath> _reactionPaths;
		private readonly ImmutableArray<Reaction> _reactions;
		private readonly IEnumerable<Reaction> _reactionsEnumerable;
		private readonly ImmutableDictionary<Command, ReactionTree> _commandToReactionTreeMappings;
		private readonly ImmutableDictionary<IEntity, Func<State, bool>> _entityToConditionMappings;

		public Reactions
		(
			Entities entities,
			Commands commands,
			ImmutableArray<ReactionMap> reactionMaps,
			ConditionInputMap? conditionAreaMap,
			ConditionInputMap? conditionItemMap
		)
		{
			// Set entities
			_entities = entities;
			// Set commands
			_commands = commands;
			// Create triggers
			var triggers = new Triggers();
			// Set reaction paths
			_reactionPaths = reactionMaps
				.Select(rm => new ReactionPath(_entities, _commands, rm))
				.ToImmutableArray();
			// Set reactions
			_reactions = reactionMaps
				.Select((rm, i) => new Reaction(triggers, _entities, _commands, _reactionPaths, _reactionPaths[i], rm.ActionMaps))
				.ToImmutableArray();
			// Validate trigger paths
			triggers.Validate();
			// Set enumerable reactions
			_reactionsEnumerable = _reactions;
			// Set command to reaction tree mappings
			_commandToReactionTreeMappings = ReactionTree.Create(_reactions);
			// Set entity to condition mappings
			_entityToConditionMappings = Enumerable.Empty<Tuple<ConditionInputMap, IEntity>>()
				.Concat
					(
						conditionAreaMap == null
						? Enumerable.Empty<Tuple<ConditionInputMap, IEntity>>()
						: entities.Areas.Select(a => new Tuple<ConditionInputMap, IEntity>(conditionAreaMap, a))
					)
				.Concat
					(
						conditionItemMap == null
						? Enumerable.Empty<Tuple<ConditionInputMap, IEntity>>()
						: entities.Items.Select(i => new Tuple<ConditionInputMap, IEntity>(conditionItemMap, i))
					)
				.Select
					(_ =>
					{
						// Get condition input map and entity
						(var conditionInputMap, var entity) = _;
						// Get input ID
						var inputId = conditionInputMap.InputId;
						// Get condition map
						var conditionMap = conditionInputMap.ConditionMap;
						// Get entity ID
						var entityId = entity.Id;
						// Create condition action
						var actionCondition = ActionCondition.Create(id => id == inputId ? entityId : id, entities, conditionMap);
						// Return ID to condition mappings
						return KeyValuePair.Create(entity, actionCondition);
					})
				.ToImmutableDictionary(IdentifiableEqualityComparer<IEntity>.Instance);
		}

		IEnumerator IEnumerable.GetEnumerator() => _reactionsEnumerable.GetEnumerator();
		public IEnumerator<Reaction> GetEnumerator() => _reactionsEnumerable.GetEnumerator();

		public ReactionResult GetResult(State state, ReactionQuery reactionQuery)
		{
			// Check if reaction query does not exist
			if (reactionQuery == null)
			{
				// Return nothing
				return ReactionResult.Nothing;
			}
			// Get command
			var command = reactionQuery.Command;
			// Try to get reaction tree
			if (!_commandToReactionTreeMappings.TryGetValue(command, out var reactionTree))
			{
				// Return nothing
				return ReactionResult.Nothing;
			}
			// Create reactions
			var reactions = reactionTree.Reactions;
			// Get parts
			var parts = reactionQuery.Parts;
			// Run through parts
			for (int i = 0; i < parts.Count; i++)
			{
				var part = parts[i];
				// Check if part is not in context
				if (_entityToConditionMappings.TryGetValue(part, out var actionCondition) && !actionCondition(state))
				{
					// Return out of context
					return ReactionResult.OutOfContext;
				}
				// Try to get next reaction tree
				if (reactionTree.Next == null || !reactionTree.Next.TryGetValue(part, out reactionTree))
				{
					// Return nothing
					return ReactionResult.Nothing;
				}
				// Set reactions
				reactions = reactionTree.Reactions;
			}
			// Check if reactions do not exist
			if (!reactions.HasValue)
			{
				// Return nothing
				return ReactionResult.Nothing;
			}
			// Return success
			return ReactionResult.Success(reactions.Value);
		}

		public Action<ResultBuilder> CreateAction(ImmutableArray<ActionMap> actionMaps, Func<Id, Id>? replacer = null)
		{
			// Create action
			var action = Actions.Create(triggers: null, _entities, _commands, _reactionPaths, reactionPath: null, actionMaps, replacer);
			// Return action
			return action;
		}
	}
}

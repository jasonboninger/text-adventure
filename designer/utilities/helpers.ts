import { ILineSpecial, SSpecial } from "../types/message";
import { XCondition, IConditionSingle, IConditionMany, SComparison, SOperator } from "../types/condition";
import { ICommandPartItem, ICommandPartArea } from "../types/command";
import { IReactionInput } from "../types/reaction";

export function area(area: string): ICommandPartArea {
	return {
		area
	};
}

export function item(item: string): ICommandPartItem {
	return {
		item
	};
}

export function input(input: string, value: string): IReactionInput {
	return {
		input,
		value
	};
}

export function blank(): ILineSpecial {
	return _special("BLANK");
}

export function horizontalRule(): ILineSpecial {
	return _special("HORIZONTAL_RULE");
}

export function is(left: string, right: string): IConditionSingle {
	return _condition(left, "IS", right);
}

export function not(left: string, right: string): IConditionSingle {
	return _condition(left, "NOT", right);
}

export function all(...conditions: XCondition[]): IConditionMany {
	return _conditions("ALL", conditions);
}

export function any(...conditions: XCondition[]): IConditionMany {
	return _conditions("ANY", conditions);
}

function _special(special: SSpecial): ILineSpecial {
	return {
		special
	};
}

function _condition(left: string, comparison: SComparison, right: string): IConditionSingle {
	return {
		left,
		comparison,
		right
	};
}

function _conditions(operator: SOperator, conditions: XCondition[]): IConditionMany {
	return {
		operator,
		conditions
	};
}

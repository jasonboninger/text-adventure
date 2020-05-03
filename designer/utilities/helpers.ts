import { ILineSpecial, XSpecial } from "../types/message";
import { XCondition, IConditionSingle, IConditionMany, XComparison, XOperator } from "../types/condition";
import { ICommandPartItem, ICommandPartArea } from "../types/command";
import { IReactionInput } from "../types/reaction";
import { IChangeStandard, IChangeCustom } from "../types/change";

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

export function standard(target: string, standard: string, value: string): IChangeStandard {
	return {
		target,
		standard,
		value
	};
}

export function custom(target: string, custom: string, value: string): IChangeCustom {
	return {
		target,
		custom,
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

function _special(special: XSpecial): ILineSpecial {
	return {
		special
	};
}

function _condition(left: string, comparison: XComparison, right: string): IConditionSingle {
	return {
		left,
		comparison,
		right
	};
}

function _conditions(operator: XOperator, conditions: XCondition[]): IConditionMany {
	return {
		operator,
		conditions
	};
}

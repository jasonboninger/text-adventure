import { ILineSpecial, SSpecial } from "../types/message";
import { ICondition, IConditions, SComparison, SOperator } from "../types/condition";

export function blank(): ILineSpecial {
	return _special("BLANK");
}

export function horizontalRule(): ILineSpecial {
	return _special("HORIZONTAL_RULE");
}

export function is(left: string, right: string): ICondition {
	return _condition(left, "IS", right);
}

export function isNot(left: string, right: string): ICondition {
	return _condition(left, "IS_NOT", right);
}

export function all(
	conditionsOne: IConditions, 
	conditionsTwo: IConditions,
	conditionsThree?: IConditions,
	conditionsFour?: IConditions,
	conditionsFive?: IConditions,
	conditionsSix?: IConditions,
	conditionsSeven?: IConditions,
	conditionsEight?: IConditions,
	conditionsNine?: IConditions,
	conditionsTen?: IConditions
): IConditions {
	return _conditions(
		"ALL",
		conditionsOne,
		conditionsTwo,
		conditionsThree,
		conditionsFour,
		conditionsFive,
		conditionsSix,
		conditionsSeven,
		conditionsEight,
		conditionsNine,
		conditionsTen
	);
}

export function any(
	conditionsOne: IConditions, 
	conditionsTwo: IConditions,
	conditionsThree?: IConditions,
	conditionsFour?: IConditions,
	conditionsFive?: IConditions,
	conditionsSix?: IConditions,
	conditionsSeven?: IConditions,
	conditionsEight?: IConditions,
	conditionsNine?: IConditions,
	conditionsTen?: IConditions
): IConditions {
	return _conditions(
		"ANY",
		conditionsOne,
		conditionsTwo,
		conditionsThree,
		conditionsFour,
		conditionsFive,
		conditionsSix,
		conditionsSeven,
		conditionsEight,
		conditionsNine,
		conditionsTen
	);
}

function _special(special: SSpecial): ILineSpecial {
	return {
		special
	};
}

function _condition(left: string, comparison: SComparison, right: string): ICondition {
	return [
		left,
		comparison,
		right
	];
}

function _conditions(
	operator: SOperator,
	conditionsOne: IConditions, 
	conditionsTwo: IConditions,
	conditionsThree?: IConditions,
	conditionsFour?: IConditions,
	conditionsFive?: IConditions,
	conditionsSix?: IConditions,
	conditionsSeven?: IConditions,
	conditionsEight?: IConditions,
	conditionsNine?: IConditions,
	conditionsTen?: IConditions
): IConditions {
	const conditions: IConditions = [
		operator,
		conditionsOne,
		conditionsTwo,
		conditionsThree,
		conditionsFour,
		conditionsFive,
		conditionsSix,
		conditionsSeven,
		conditionsEight,
		conditionsNine,
		conditionsTen
	];
	while (conditions.length && conditions[conditions.length - 1] === undefined) conditions.pop();
	return conditions;
}

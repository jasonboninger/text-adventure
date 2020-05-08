export type XComparison = "IS" | "NOT";
export type XOperator = "ALL" | "ANY";

export type XCondition = IConditionSingle | IConditionMany

export interface IConditionArea {
	area: string;
	condition: XCondition;
}

export interface IConditionItem {
	item: string;
	condition: XCondition;
}

export interface IConditionSingle {
	left: string;
	comparison: XComparison;
	right: string;
	operator?: never;
	conditions?: never;
};

export interface IConditionMany {
	left?: never;
	comparison?: never;
	right?: never;
	operator: XOperator;
	conditions: XCondition[];
};

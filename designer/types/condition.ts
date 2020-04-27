export type SComparison = "IS" | "NOT";
export type SOperator = "ALL" | "ANY";

export type XCondition = IConditionSingle | IConditionMany

export interface IConditionSingle {
	left: string;
	comparison: SComparison;
	right: string;
	operator?: never;
	conditions?: never;
};

export interface IConditionMany {
	left?: never;
	comparison?: never;
	right?: never;
	operator: SOperator;
	conditions: XCondition[];
};

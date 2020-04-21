export type SComparison = "IS" | "NOT";
export type ICondition = [string, SComparison, string];
export type SOperator = "ALL" | "ANY";
export type IConditions = 
	ICondition
	| [SOperator, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [SOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]

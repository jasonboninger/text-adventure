export type IComparison = "==" | "!=";
export type ICondition = string | [string, IComparison, string];
export type IOperator = "AND" | "OR";
export type IConditions = 
	ICondition 
	| [IOperator, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]
	| [IOperator, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions, IConditions]

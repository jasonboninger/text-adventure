import { IDictionary } from "./core";

export type XFunctionCondition = string | IFunctionCondition;

export interface IFunction {
	set?: IFunctionSet;
	if?: IFunctionIf
}

export interface IFunctionSet {
	item?: { id: string } & IDictionary<string>;
	area?: { id: string } & IDictionary<string>;
	player?: IDictionary<string>;
	game?: IDictionary<string>;
}

export interface IFunctionIf {
	condition: XFunctionCondition;
	true?: IFunction;
	false?: IFunction;
}

export interface IFunctionCondition {
	test: string;
	and: IFunctionCondition[]
	or: IFunctionCondition[];
}

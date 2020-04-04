import { XMessage } from "./message";
import { IOneOrArray, IOneOrDictionary } from "./core";
import { IFunction, IFunctionCondition } from "./function";

export type XActions = IActions | IAction;

export interface IActions {
	tests?: IOneOrDictionary<IFunctionCondition>;
	actions: IOneOrArray<IAction>;
}

export interface IAction {
	tests?: IOneOrDictionary<IFunctionCondition>;
	messages?: IOneOrArray<XMessage>;
	functions?: IOneOrArray<IFunction>;
}

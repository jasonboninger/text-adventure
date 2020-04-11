import { XMessage } from "./message";
import { IOneOrArray, IDictionary } from "./core";
import { IConditions } from "./condition";

export type XAction = IActionIIf | IActionMessage | IActionChanges | IActionTriggers;

export interface IAction {
	tests?: IDictionary<IConditions>;
}

export interface IActionIIf extends IAction {
	if: IIf;
	messages?: never;
	changes?: never;
	triggers?: never;
}

export interface IActionMessage extends IAction {
	if?: never;
	messages: IOneOrArray<XMessage>;
	changes?: never;
	triggers?: never;
}

export interface IActionChanges extends IAction {
	if?: never;
	messages?: never;
	changes: IDictionary<string>;
	triggers?: never;
}

export interface IActionTriggers extends IAction {
	if?: never;
	messages?: never;
	changes?: never;
	triggers: IDictionary<IDictionary<string>>;
}

export interface IIf {
	condition: IConditions;
	true?: IOneOrArray<XAction>;
	false?: IOneOrArray<XAction>;
}

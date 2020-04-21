import { XMessage } from "./message";
import { IOneOrArray, IDictionary, IIf } from "./core";
import { ITrigger } from "./trigger";

export type XAction = IActionIf | IActionMessage | IActionChanges | IActionTriggers;

export interface IActionIf {
	if: IIf<XAction>;
	messages?: never;
	changes?: never;
	triggers?: never;
}

export interface IActionMessage {
	if?: never;
	messages: IOneOrArray<XMessage>;
	changes?: never;
	triggers?: never;
}

export interface IActionChanges {
	if?: never;
	messages?: never;
	changes: IDictionary<string>;
	triggers?: never;
}

export interface IActionTriggers {
	if?: never;
	messages?: never;
	changes?: never;
	triggers: IOneOrArray<ITrigger>;
}

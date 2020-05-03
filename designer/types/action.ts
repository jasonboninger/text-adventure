import { XMessage } from "./message";
import { XOneOrArray, IIf } from "./core";
import { XChange } from "./change";
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
	messages: XOneOrArray<XMessage>;
	changes?: never;
	triggers?: never;
}

export interface IActionChanges {
	if?: never;
	messages?: never;
	changes: XOneOrArray<XChange>;
	triggers?: never;
}

export interface IActionTriggers {
	if?: never;
	messages?: never;
	changes?: never;
	triggers: XOneOrArray<ITrigger>;
}

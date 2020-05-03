import { XMessage } from "./message";
import { XOneOrArray, IIf } from "./core";
import { XChange } from "./change";
import { ITrigger } from "./trigger";
import { XIterator } from "./iterator";

export type XAction = IActionIterator | IActionIf | IActionMessage | IActionChanges | IActionTriggers;

export interface IActionIterator {
	iterators: XOneOrArray<XIterator>;
	if?: never;
	messages?: never;
	changes?: never;
	triggers?: never;
}

export interface IActionIf {
	iterators?: never;
	if: IIf<XAction>;
	messages?: never;
	changes?: never;
	triggers?: never;
}

export interface IActionMessage {
	iterators?: never;
	if?: never;
	messages: XOneOrArray<XMessage>;
	changes?: never;
	triggers?: never;
}

export interface IActionChanges {
	iterators?: never;
	if?: never;
	messages?: never;
	changes: XOneOrArray<XChange>;
	triggers?: never;
}

export interface IActionTriggers {
	iterators?: never;
	if?: never;
	messages?: never;
	changes?: never;
	triggers: XOneOrArray<ITrigger>;
}

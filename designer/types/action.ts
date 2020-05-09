import { XMessage } from "./message";
import { XOneOrArray, IIf, XIterator } from "./core";
import { XChange } from "./change";
import { ITrigger } from "./trigger";

export type XActionSpecial = "END";

export type XAction = IActionIterator | IActionIf | IActionMessage | IActionChanges | IActionTriggers | IActionSpecial;

export interface IActionIterator {
	iterators: XOneOrArray<XIterator<XAction>>;
	if?: never;
	messages?: never;
	changes?: never;
	triggers?: never;
	special?: never;
}

export interface IActionIf {
	iterators?: never;
	if: IIf<XAction>;
	messages?: never;
	changes?: never;
	triggers?: never;
	special?: never;
}

export interface IActionMessage {
	iterators?: never;
	if?: never;
	messages: XOneOrArray<XMessage>;
	changes?: never;
	triggers?: never;
	special?: never;
}

export interface IActionChanges {
	iterators?: never;
	if?: never;
	messages?: never;
	changes: XOneOrArray<XChange>;
	triggers?: never;
	special?: never;
}

export interface IActionTriggers {
	iterators?: never;
	if?: never;
	messages?: never;
	changes?: never;
	triggers: XOneOrArray<ITrigger>;
	special?: never;
}

export interface IActionSpecial {
	iterators?: never;
	if?: never;
	messages?: never;
	changes?: never;
	triggers?: never;
	special: XActionSpecial;
}

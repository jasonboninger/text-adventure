import { XOneOrArray, IIf, XIterator } from "./core";

export type XMessage = string | IMessage;
export type XLine = string | ILineIterator | ILineIf | ILineSpecial | ILine;
export type XText = string | ITextIterator | ITextIf | IText
export type XSpecial = "BLANK" | "HORIZONTAL_RULE";

export interface IMessage {
	lines: XOneOrArray<XLine>;
}

export interface ILineIterator {
	iterators: XOneOrArray<XIterator<XLine>>;
	if?: never;
	special?: never;
	texts?: never;
}

export interface ILineIf {
	iterators?: never;
	if: IIf<XLine>;
	special?: never;
	texts?: never;
}

export interface ILineSpecial {
	iterators?: never;
	if?: never;
	special: XSpecial;
	texts?: never;
}

export interface ILine {
	iterators?: never;
	if?: never;
	special?: never;
	texts: XOneOrArray<XText>;
}

export interface ITextIterator {
	iterators: XOneOrArray<XIterator<XText>>;
	if?: never;
	value?: never;
}

export interface ITextIf {
	iterators?: never;
	if: IIf<XText>;
	value?: never;
}

export interface IText {
	iterators?: never;
	if?: never;
	value: string;
}

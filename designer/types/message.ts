import { IOneOrArray, IIf } from "./core";

export type XMessage = string | IMessage;
export type XLine = string | ILineIf | ILineSpecial | ILine;
export type XText = string | ITextIf | IText
export type SSpecial = "BLANK" | "HORIZONTAL_RULE";

export interface IMessage {
	lines: IOneOrArray<XLine>;
}

export interface ILineIf {
	if: IIf<XLine>;
	special?: never;
	texts?: never;
}

export interface ILineSpecial {
	if?: never;
	special: SSpecial;
	texts?: never;
}

export interface ILine {
	if?: never;
	special?: never;
	texts: IOneOrArray<XText>;
}

export interface ITextIf {
	if: IIf<XText>;
	value?: never;
}

export interface IText {
	if?: never;
	value: string;
}

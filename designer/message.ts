import { IOneOrDictionary } from "./core";

export type XMessage = string | IMessageRaw | IMessageFormatted;
export type XLine = string | ILine | ISpecial;

export interface IMessageRaw {
	lines: XLine | XLine[];
}

export interface IMessageFormatted {
	template: string;
	inputs: IOneOrDictionary<XMessage>
}

export interface ILine {
	text: string;
}

export interface ISpecial {
	special: "horizontal-rule" | "blank";
}

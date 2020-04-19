import { IOneOrArray, IDictionary } from "./core";

export type XMessage = string | IMessageInlined | IMessageTemplated | IInput;
export type XLine = string | ILine | ISpecial | IInput;

export interface IMessageInlined {
	lines: IOneOrArray<XLine>;
}

export interface IMessageTemplated {
	template: string;
	messages?: IDictionary<IOneOrArray<XMessage>>;
	lines?: IDictionary<IOneOrArray<XLine>>;
	texts?: IDictionary<string>;
}

export interface ILine {
	text: string;
}

export interface ISpecial {
	special: "HorizontalRule" | "Blank";
}

export interface IInput {
	input: string;
}

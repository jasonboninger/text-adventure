import { IOneOrArray, IDictionary } from "./core";

export type XMessage = string | IMessageInlined | IMessageTemplated | IInput;
export type XLine = string | ILine | ISpecial | IInput;

export function HORIZONTAL_RULE(): ISpecial {
	return {
		special: "HORIZONTAL_RULE"
	};
}

export function BLANK(): ISpecial {
	return {
		special: "BLANK"
	};
}

export function INPUT(input: string): IInput {
	return {
		input
	};
}

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
	special: "HORIZONTAL_RULE" | "BLANK";
}

export interface IInput {
	input: string;
}

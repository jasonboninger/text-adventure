import { XOneOrArray } from "./core";
import { XAction } from "./action";

export type XCommandPart = string[] | ICommandPartWord | ICommandPartArea | ICommandPartItem

export interface ICommand {
	id: string;
	parts: XCommandPart[];
	fail?: XOneOrArray<XAction>;
}

export interface ICommandPartWord {
	words: string[];
	area?: never;
	item?: never;
}

export interface ICommandPartArea {
	words?: never;
	area: string;
	item?: never;
}

export interface ICommandPartItem {
	words?: never;
	area?: never;
	item: string;
}

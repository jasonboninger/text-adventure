import { XOneOrArray } from "./core";
import { XAction } from "./action";

export type XCommandPart = string[] | ICommandPartWord | ICommandPartPlayer | ICommandPartArea | ICommandPartItem

export interface ICommand {
	id: string;
	parts: XCommandPart[];
	fallback?: XOneOrArray<XAction>;
}

export interface ICommandPartWord {
	words: string[];
	player?: never;
	area?: never;
	item?: never;
}

export interface ICommandPartPlayer {
	words?: never;
	player: string;
	area?: never;
	item?: never;
}

export interface ICommandPartArea {
	words?: never;
	player?: never;
	area: string;
	item?: never;
}

export interface ICommandPartItem {
	words?: never;
	player?: never;
	area?: never;
	item: string;
}

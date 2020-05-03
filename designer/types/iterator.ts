import { XOneOrArray } from "./core";
import { XAction } from "./action";

export type XIterator = IIteratorArea | IIteratorItem;

export interface IIteratorArea extends IIterator {
	area: string;
	item?: never;
}

export interface IIteratorItem extends IIterator {
	area?: never;
	item: string;
}

export interface IIterator {
	actions: XOneOrArray<XAction>;
}
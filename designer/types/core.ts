import { XCondition } from "./condition";

export type XOneOrArray<TValue> = TValue | TValue[];

export interface IIf<TValue> {
	condition: XCondition;
	true?: XOneOrArray<TValue>;
	false?: XOneOrArray<TValue>;
};

export type XIterator<TValue> = IIteratorArea<TValue> | IIteratorItem<TValue>;

export interface IIteratorArea<TValue> extends IIterator<TValue> {
	area: string;
	item?: never;
}

export interface IIteratorItem<TValue> extends IIterator<TValue> {
	area?: never;
	item: string;
}

export interface IIterator<TValue> {
	processor: XOneOrArray<TValue>;
}

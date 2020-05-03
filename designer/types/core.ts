import { XCondition } from "./condition";

export type XDictionary<TValue> = { [id: string]: TValue };

export type XOneOrArray<TValue> = TValue | TValue[];

export interface IIf<TValue> {
	condition: XCondition;
	true?: XOneOrArray<TValue>;
	false?: XOneOrArray<TValue>;
};

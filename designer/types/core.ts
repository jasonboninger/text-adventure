import { XCondition } from "./condition";

export type XOneOrArray<TValue> = TValue | TValue[];

export interface IIf<TValue> {
	condition: XCondition;
	true?: XOneOrArray<TValue>;
	false?: XOneOrArray<TValue>;
};

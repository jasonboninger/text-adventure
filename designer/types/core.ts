import { ICondition } from "./condition";

export type IDictionary<TValue> = { [id: string]: TValue };

export type IOneOrArray<TValue> = TValue | TValue[];

export type IIf<TValue> = {
	condition: ICondition;
	true?: IOneOrArray<TValue>;
	false?: IOneOrArray<TValue>;
};

import { IConditions } from "./condition";

export type IDictionary<TValue> = { [id: string]: TValue };

export type IOneOrArray<TValue> = TValue | TValue[];

export type IOneOrDictionary<TValue> = TValue | IDictionary<TValue>;

export type IIf<TValue> = {
	condition: IConditions;
	true?: IOneOrArray<TValue>;
	false?: IOneOrArray<TValue>;
};

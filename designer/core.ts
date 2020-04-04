export type IDictionary<TValue> = { [id: string]: TValue };

export type IOneOrArray<TValue> = TValue | TValue[];

export type IOneOrDictionary<TValue> = TValue | IDictionary<TValue>;

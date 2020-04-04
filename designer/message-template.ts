import { XMessage } from "./message";
import { IOneOrArray, IDictionary } from "./core";

export interface IMessageTemplate {
	template: IOneOrArray<XMessage>;
	inputs?: IDictionary<IInput>;
}

export interface IInput {
	required: boolean;
}

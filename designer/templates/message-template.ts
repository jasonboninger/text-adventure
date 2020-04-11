import { IOneOrArray } from "../core";
import { XMessage } from "../message";

export interface IMessageTemplate {
	template: IOneOrArray<XMessage>;
	messages?: string[];
	lines?: string[];
	texts?: string[];
}

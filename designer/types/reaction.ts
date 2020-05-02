import { IOneOrArray } from "./core";
import { XAction } from "./action";

export interface IReaction {
	command: string;
	inputs?: IReactionInput[];
	actions: IOneOrArray<XAction>;
}

export interface IReactionInput {
	input: string;
	value: string;
}

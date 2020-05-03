import { XOneOrArray } from "./core";
import { XAction } from "./action";

export interface IReaction {
	command: string;
	inputs?: IReactionInput[];
	actions: XOneOrArray<XAction>;
}

export interface IReactionInput {
	input: string;
	value: string;
}

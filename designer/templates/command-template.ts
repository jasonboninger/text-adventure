import { IDictionary } from "../core";

export interface ICommandTemplate {
	template: string[];
	words: IDictionary<string[]>;
	items?: string[];
}

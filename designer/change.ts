import { IDictionary } from "./core";

export interface IChange {
	item?: { id: string } & IDictionary<string>;
	area?: { id: string } & IDictionary<string>;
	player?: IDictionary<string>;
	game?: IDictionary<string>;
}

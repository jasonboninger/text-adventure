import { IDictionary, IOneOrArray } from "./core";
import { ICommand } from "./command";
import { IPlayer } from "./player";
import { IArea } from "./area";
import { XAction } from "./action";

export interface IGame {
	commands: IDictionary<ICommand>;
	player: IPlayer;
	areas: IDictionary<IArea>;
	start?: IOneOrArray<XAction>;
	end?: IOneOrArray<XAction>;
}

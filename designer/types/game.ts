import { IOneOrArray } from "./core";
import { ICommand } from "./command";
import { IPlayer } from "./player";
import { IArea } from "./area";
import { XAction } from "./action";

export interface IGame {
	commands: ICommand[];
	player: IPlayer;
	areas: IArea[];
	start?: IOneOrArray<XAction>;
	end?: IOneOrArray<XAction>;
}

import * as fs from "fs";
import * as cli from "child_process";
import { GAME } from "./target";

const action = process.argv[2];
const folder = process.argv[3];

// Check if folder does not exist
if (!fs.existsSync(folder)) {
	// Create folder
	fs.mkdirSync(folder);
}

// Check if API
if (action === "api") {
	// Build API
	cli.execSync(`dotnet publish ../api/BoningerWorks.TextAdventure.Cli -o ${folder}`)
}

// Check if game
if (action === "game") {
	// Build game
	const game = JSON.stringify(GAME);
	// Write game
	fs.writeFileSync(`${folder}/game.json`, game);
}

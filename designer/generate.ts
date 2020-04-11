import * as fs from "fs";
import { CRAZY_EX } from "./crazy-ex";

const folderPath = process.argv[2];

const crazyEx = CRAZY_EX;
const crazyExJson = JSON.stringify(crazyEx, undefined, "\t");
const crazyExCs = 
`namespace BoningerWorks.TextAdventure.Engine.Generated
{
	public static class CrazyEx
	{
		public const string JSON = 
@"${crazyExJson.replace(/"/g, "\"\"")}";
	}
}
`;

fs.writeFileSync(folderPath + "/CrazyEx.cs", crazyExCs);

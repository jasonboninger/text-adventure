import { NgModule } from "@angular/core";
import { Routes, RouterModule, PreloadAllModules } from "@angular/router";

import { ERoute } from "@models/route.enum";
import { ShellComponent } from "./components/shell/shell.component";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";

const routes: Routes = [
	{
		path: "", component: ShellComponent, children: [
			{
				path: ERoute.home,
				loadChildren: () => import("./modules/home/home.module").then(m => m.HomeModule)
			},
			{ path: "", redirectTo: ERoute.home, pathMatch: "full" }
		]
	},
	{ path: "**", component: PageNotFoundComponent }
];

@NgModule({
	imports: [RouterModule.forRoot(routes, {
		preloadingStrategy: PreloadAllModules,
		scrollPositionRestoration: "enabled"
	})],
	exports: [RouterModule]
})
export class AppRoutingModule { }

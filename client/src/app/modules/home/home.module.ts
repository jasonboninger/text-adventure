import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

import { ERoute } from "@app/models/route.enum";
import { SharedModule } from "../shared/shared.module";
import { HomeComponent } from "./home.component";



const homeRoutes = [
	{ path: "", component: HomeComponent, data: { state: ERoute.home } }
];

@NgModule({
	declarations: [
		HomeComponent
	],
	imports: [
		CommonModule,
		RouterModule.forChild(homeRoutes),
		SharedModule,
	]
})
export class HomeModule { }

import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ShellComponent } from "./components/shell/shell.component";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";

@NgModule({
	declarations: [
		AppComponent,
		ShellComponent,
		PageNotFoundComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }

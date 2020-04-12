import { NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { SharedModule } from "./modules/shared/shared.module";
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
		BrowserAnimationsModule,
		HttpClientModule,
		AppRoutingModule,
		SharedModule,
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }

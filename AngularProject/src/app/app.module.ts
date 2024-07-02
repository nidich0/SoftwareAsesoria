import { NgModule } from "@angular/core";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from "./app.component";
import { provideHttpClient } from '@angular/common/http';
import { LoginComponent } from "./routes/login/login.component";
import { SignupComponent } from "./routes/signup/signup.component";
import { HomeComponent } from "./routes/home/home.component";
import { SolicitarComponent } from "./routes/solicitar/solicitar.component";
import { AsesoresComponent } from "./routes/asesores/asesores.component";
import { AppLayoutModule } from "./layout/app.layout.module";
import { TranslocoRootModule } from './transloco-root.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        TranslocoRootModule,
        LoginComponent,
        SignupComponent,
        HomeComponent,
        SolicitarComponent,
        AsesoresComponent,
    ],
    providers: [provideHttpClient()],
    bootstrap: [AppComponent],
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/auth.guard';

import { AppLayoutComponent } from './layout/app.layout.component';
import { LoginComponent } from './routes/login/login.component';
import { SignupComponent } from './routes/signup/signup.component';
import { HomeComponent } from './routes/home/home.component';
import { SolicitarComponent } from './routes/solicitar/solicitar.component';
import { AsesoresComponent } from './routes/asesores/asesores.component';

const routes: Routes = [
    {
        path: '', component: AppLayoutComponent,
        children: [
            { path: '', component: HomeComponent }
        ]
    },
    {
        path: 'login', component: LoginComponent
    },
    {
        path: 'signup', component: SignupComponent
    },
    {
        path: 'solicitar', component: AppLayoutComponent, canActivate: [authGuard],
        children: [
            { path: '', component: SolicitarComponent }
        ]
    },
    {
        path: 'asesores', component: AppLayoutComponent, canActivate: [authGuard],
        children: [
            { path: '', component: AsesoresComponent }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }

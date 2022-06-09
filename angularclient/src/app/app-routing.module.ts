import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './identity/pages/login/login.component';
import { RegisterComponent } from './identity/pages/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginGuard } from './core/guards/login.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', component: LoginComponent, canActivate: [LoginGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [LoginGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

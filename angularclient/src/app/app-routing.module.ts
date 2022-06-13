import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './identity/login/login.component';
import { RegisterComponent } from './identity/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginGuard } from './core/guards/login.guard';
import { IdentityComponent } from './identity/identity.component';
import { EmailConfirmationComponent } from './identity/email-confirmation/email-confirmation.component';
import { EmailConfirmedComponent } from './identity/email-confirmed/email-confirmed.component';

const routes: Routes = [
  {
    path: 'identity',
    component: IdentityComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ],
    canActivate: [LoginGuard],
  },
  {
    path: 'emailconfirmation',
    component: EmailConfirmationComponent,
  },
  {
    path: 'emailconfirmed',
    component: EmailConfirmedComponent,
  },
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

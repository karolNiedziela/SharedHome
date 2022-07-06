import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailConfirmedComponent } from './email-confirmed/email-confirmed.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { LoginGuard } from 'app/core/guards/login.guard';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { IdentityComponent } from './identity.component';

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
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}

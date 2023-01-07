import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { IdentityComponent } from './identity.component';
import { RegisterComponent } from './register/register.component';
import { LoginGuard } from 'src/app/core/guards/login.guard';

const routes: Routes = [
  {
    path: 'identity',
    component: IdentityComponent,
    children: [
      { path: 'login', component: LoginComponent, canActivate: [LoginGuard] },
      {
        path: 'register',
        component: RegisterComponent,
        canActivate: [LoginGuard],
      },
    ],
  },
  //     {
  //       path: 'resetpassword',
  //       component: ResetPasswordComponent,
  //     },
  //     {
  //       path: 'forgotpassword',
  //       component: ForgotPasswordComponent,
  //     },
  //   ],
  // },
  // {
  //   path: 'emailconfirmation',
  //   component: EmailConfirmationComponent,
  // },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IdentityRoutingModule {}

import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { IdentityComponent } from './identity.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { IdentityRoutingModule } from './identity-routing.module';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
  declarations: [
    IdentityComponent,
    LoginComponent,
    RegisterComponent,
    EmailConfirmationComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
  ],
  imports: [SharedModule, IdentityRoutingModule, TranslateModule],
})
export class IdentityModule {}

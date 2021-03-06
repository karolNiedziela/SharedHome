import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { EmailConfirmedComponent } from './email-confirmed/email-confirmed.component';
import { IdentityComponent } from './identity.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { IdentityRoutingModule } from './identity-routing.module';

@NgModule({
  declarations: [
    IdentityComponent,
    LoginComponent,
    RegisterComponent,
    EmailConfirmationComponent,
    EmailConfirmedComponent,
  ],
  imports: [SharedModule, IdentityRoutingModule],
})
export class IdentityModule {}

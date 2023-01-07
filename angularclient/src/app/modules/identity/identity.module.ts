import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { IdentityRoutingModule } from './identity-routing.module';
import { IdentityComponent } from './identity.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [IdentityComponent, LoginComponent, RegisterComponent],
  imports: [SharedModule, IdentityRoutingModule, TranslateModule],
})
export class IdentityModule {}

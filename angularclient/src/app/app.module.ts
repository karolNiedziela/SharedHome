import { AuthenticationResultInterceptor } from './core/interceptors/authentication-result.interceptor';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './identity/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './identity/register/register.component';
import { ButtonComponent } from './shared/components/button/button.component';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { HomeComponent } from './home/home.component';
import { TextInputComponent } from './shared/components/inputs/text-input/text-input.component';
import { EmailInputComponent } from './shared/components/inputs/email-input/email-input.component';
import { PasswordInputComponent } from './shared/components/inputs/password-input/password-input.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PasswordsFormComponent } from './shared/components/forms/passwords-form/passwords-form.component';
import { EmailConfirmationComponent } from './identity/email-confirmation/email-confirmation.component';
import { EmailConfirmedComponent } from './identity/email-confirmed/email-confirmed.component';
import { IdentityComponent } from './identity/identity.component';

@NgModule({
  declarations: [
    AppComponent,
    IdentityComponent,
    LoginComponent,
    RegisterComponent,
    ButtonComponent,
    HomeComponent,
    TextInputComponent,
    EmailInputComponent,
    PasswordInputComponent,
    PasswordsFormComponent,
    EmailConfirmationComponent,
    EmailConfirmedComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    FontAwesomeModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationResultInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

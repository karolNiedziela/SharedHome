import { faEyeSlash } from './../../node_modules/@fortawesome/free-solid-svg-icons/faEyeSlash.d';
import { library } from './../../node_modules/@fortawesome/fontawesome-svg-core/index.d';
import { AuthenticationResultInterceptor } from './core/interceptors/authentication-result.interceptor';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './identity/pages/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './identity/pages/register/register.component';
import { ButtonComponent } from './shared/components/button/button.component';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { HomeComponent } from './home/home.component';
import { TextInputComponent } from './shared/components/inputs/text-input/text-input.component';
import { EmailInputComponent } from './shared/components/inputs/email-input/email-input.component';
import { PasswordInputComponent } from './shared/components/inputs/password-input/password-input.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PasswordsFormComponent } from './shared/components/forms/passwords-form/passwords-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ButtonComponent,
    HomeComponent,
    TextInputComponent,
    EmailInputComponent,
    PasswordInputComponent,
    PasswordsFormComponent,
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

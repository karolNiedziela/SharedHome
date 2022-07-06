import { BillsModule } from './modules/bills/bills.module';
import { IdentityModule } from './modules/identity/identity.module';
import { ShoppingListsModule } from './modules/shopping-lists/shopping-lists.module';
import { SharedModule } from './shared/shared.module';
import { AuthenticationResultInterceptor } from './core/interceptors/authentication-result.interceptor';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FontAwesomeModule,
    AppRoutingModule,
    SharedModule,
    ShoppingListsModule,
    BillsModule,
    AppRoutingModule,
    IdentityModule,
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

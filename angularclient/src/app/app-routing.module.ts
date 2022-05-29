import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './identity/pages/login/login.component';
import { RegisterComponent } from './identity/pages/register/register.component';

const routes: Routes = [{ path: '', component: LoginComponent },
{ path: 'register', component: RegisterComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

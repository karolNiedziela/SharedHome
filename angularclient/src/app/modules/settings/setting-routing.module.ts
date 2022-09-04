import { SettingComponent } from './setting/setting.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'app/core/guards/auth.guard';

const routes: Routes = [
  { path: 'settings', component: SettingComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SettingRoutingModule {}

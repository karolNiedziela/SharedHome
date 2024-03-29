import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { HousegroupMembersComponent } from './housegroup-members/housegroup-members.component';

const routes: Routes = [
  {
    path: 'housegroups',
    component: HousegroupMembersComponent,
    canActivate: [AuthGuard],
    title: 'house_groups.module',
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HousegroupRoutingModule {}

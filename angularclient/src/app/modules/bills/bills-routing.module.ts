import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BillsComponent } from './bills.component';
import { AuthGuard } from 'app/core/guards/auth.guard';

const routes: Routes = [
  {
    path: 'bills',
    component: BillsComponent,
    canActivate: [AuthGuard],
    title: 'Bills',
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BillsRoutingModule {}

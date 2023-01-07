import { BillsCalendarComponent } from './bills-calendar/bills-calendar.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from 'src/app/core/guards/auth.guard';

const routes: Routes = [
  {
    path: 'bills',
    component: BillsCalendarComponent,
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

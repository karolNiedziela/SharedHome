import { Component } from '@angular/core';
import {
  faBars,
  faBell,
  faCartShopping,
  faChartColumn,
  faEnvelope,
  faFileInvoice,
  faGear,
  faSignOut,
  faUserGroup,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
  barsIcon = faBars;
  shoppingListsIcon = faCartShopping;
  billsIcon = faFileInvoice;
  houseGroupIcon = faUserGroup;
  expensesIcon = faChartColumn;
  invitationsIcon = faEnvelope;
  notificationsIcon = faBell;
  settingsIcon = faGear;
  logoutIcon = faSignOut;

  constructor() {}
}

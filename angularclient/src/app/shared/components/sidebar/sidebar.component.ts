import { Component, OnInit, OnDestroy } from '@angular/core';
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
import { AuthenticationService } from 'app/modules/identity/services/authentication.service';

declare var bootstrap: any;

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

  private tooltipList = new Array<any>();

  constructor(private authenticationService: AuthenticationService) {}

  toggleMenu(): void {
    let sidebar = document.querySelector('.sidebar');
    sidebar?.classList.toggle('is-active');

    const menuToggle = document.querySelector('.menu-toggle');
    menuToggle?.classList.toggle('is-active');
  }

  logout() {
    this.authenticationService.logout();
  }
}

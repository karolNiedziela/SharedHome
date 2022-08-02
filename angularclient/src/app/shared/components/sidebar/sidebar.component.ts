import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
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
  encapsulation: ViewEncapsulation.None,
})
export class SidebarComponent implements OnInit, OnDestroy {
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

  ngOnInit(): void {
    this.enableTooltip();
  }

  ngOnDestroy(): void {
    this.hideAllTooltips();
  }

  toggleMenu(): void {
    let sidebar = document.querySelector('.sidebar');
    sidebar?.classList.toggle('active');
    sidebar?.classList.contains('active')
      ? this.hideAllTooltips()
      : this.enableTooltip();
  }

  enableTooltip(): void {
    // Bootstrap tooltip initialization
    var tooltipTriggerList = [].slice.call(
      document.querySelectorAll('[data-bs-toggle="tooltip"]')
    );

    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
      return new bootstrap.Tooltip(tooltipTriggerEl, {
        trigger: 'hover',
      });
    });

    this.tooltipList.push(...tooltipList);
  }

  hideAllTooltips(): void {
    for (const tooltip of this.tooltipList) {
      tooltip.dispose();
    }
    this.tooltipList = new Array<any>();
  }

  logout() {
    this.authenticationService.logout();
  }
}

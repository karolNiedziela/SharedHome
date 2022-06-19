import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  faBars,
  faCartShopping,
  faChartColumn,
  faEnvelope,
  faFileInvoice,
  faSignOut,
  faUserGroup,
} from '@fortawesome/free-solid-svg-icons';

declare var bootstrap: any;

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SidebarComponent implements OnInit {
  barsIcon = faBars;
  shoppingListsIcon = faCartShopping;
  billsIcon = faFileInvoice;
  houseGroupIcon = faUserGroup;
  expensesIcon = faChartColumn;
  invitationsIcon = faEnvelope;
  logoutIcon = faSignOut;

  private tooltipList = new Array<any>();

  constructor() {}

  ngOnInit(): void {
    this.enableTooltip();
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
      return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    this.tooltipList.push(...tooltipList);
  }

  hideAllTooltips(): void {
    this.tooltipList;
    for (const tooltip of this.tooltipList) {
      tooltip.dispose();
    }
    this.tooltipList = new Array<any>();
  }
}

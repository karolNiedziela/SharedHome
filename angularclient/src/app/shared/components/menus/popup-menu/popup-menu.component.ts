import { Component, Input, OnInit } from '@angular/core';
import { PopupMenuConfig } from './popup-menu.config';

@Component({
  selector: 'app-popup-menu',
  templateUrl: './popup-menu.component.html',
  styleUrls: ['./popup-menu.component.scss'],
})
export class PopupMenuComponent implements OnInit {
  @Input() public popupMenuConfig?: PopupMenuConfig;

  constructor() {}

  ngOnInit(): void {
    if (this.popupMenuConfig == null || this.popupMenuConfig == undefined) {
      this.popupMenuConfig = {
        isEditVisible: true,
        isDeleteVisible: true,
        isHidden: false,
      };
    }
  }
}

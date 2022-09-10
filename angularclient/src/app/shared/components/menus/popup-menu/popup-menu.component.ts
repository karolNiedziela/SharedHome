import { PopupMenuConfig } from './popup-menu.config';
import { Component, Input, OnInit } from '@angular/core';
import { faEllipsisVertical } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-popup-menu',
  templateUrl: './popup-menu.component.html',
  styleUrls: ['./popup-menu.component.scss'],
})
export class PopupMenuComponent implements OnInit {
  @Input() public popupMenuConfig!: PopupMenuConfig;

  moreOptionsIcon = faEllipsisVertical;

  constructor() {}

  ngOnInit(): void {
    if (
      this.popupMenuConfig.isHidden == null ||
      this.popupMenuConfig.isHidden == undefined
    ) {
      this.popupMenuConfig.isHidden = false;
    }

    if (
      this.popupMenuConfig.isEditVisible == null ||
      this.popupMenuConfig.isEditVisible == undefined
    ) {
      this.popupMenuConfig.isEditVisible = true;
    }

    if (
      this.popupMenuConfig.isDeleteVisible == null ||
      this.popupMenuConfig.isDeleteVisible == undefined
    ) {
      this.popupMenuConfig.isDeleteVisible = true;
    }
  }
}

import {
  faCheck,
  faCircleInfo,
  faEllipsisVertical,
} from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-shopping-lists',
  templateUrl: './shopping-lists.component.html',
  styleUrls: ['./shopping-lists.component.scss'],
})
export class ShoppingListsComponent implements OnInit {
  previewIcon = faCircleInfo;
  moreOptionsIcon = faEllipsisVertical;
  boughtIcon = faCheck;

  constructor() {}

  ngOnInit(): void {}
}

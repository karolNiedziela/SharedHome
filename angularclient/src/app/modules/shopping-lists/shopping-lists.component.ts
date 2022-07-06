import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  faCheck,
  faCircleInfo,
  faEllipsisVertical,
} from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { yearAndMonthFormat } from 'app/shared/validators/dateformat.validator';

@Component({
  selector: 'app-shopping-lists',
  templateUrl: './shopping-lists.component.html',
  styleUrls: ['./shopping-lists.component.scss'],
})
export class ShoppingListsComponent implements OnInit {
  previewIcon = faCircleInfo;
  moreOptionsIcon = faEllipsisVertical;
  boughtIcon = faCheck;

  shoppingListForm!: FormGroup;

  constructor() {
    const currentYearAndMonth = `${new Date().getFullYear()} ${new Date().getMonth()}`;
    this.shoppingListForm = new FormGroup({
      yearAndMonth: new FormControl(currentYearAndMonth, [
        Validators.required,
        yearAndMonthFormat,
      ]),
    });
  }

  ngOnInit(): void {}

  onCurrentYearAndMonthChanged(): void {
    console.log('hello');
  }
}

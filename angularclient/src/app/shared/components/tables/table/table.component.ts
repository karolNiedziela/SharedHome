import { PopupMenuConfig } from './../../menus/popup-menu/popup-menu.config';
import { Component, OnInit, Input } from '@angular/core';
import { ColumnSetting } from '../column-setting';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent<T> implements OnInit {
  @Input() records!: any[];
  @Input() settings?: ColumnSetting[];
  @Input() actions?: PopupMenuConfig[];

  columnMaps?: ColumnSetting[];

  constructor() {}

  ngOnInit(): void {
    if (this.settings) {
      this.columnMaps = this.settings;
    } else {
      this.columnMaps = Object.keys(this.records[0]).map((key) => {
        return {
          propertyName: key,
          header:
            key.slice(0, 1).toUpperCase() + key.replace(/_/g, ' ').slice(1),
          hidden: false,
          format: 0,
        };
      });
    }
  }
}

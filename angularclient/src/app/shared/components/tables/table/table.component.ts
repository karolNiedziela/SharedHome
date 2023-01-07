import { Component, Input, OnInit } from '@angular/core';
import { PopupMenuConfig } from '../../menus/popup-menu/popup-menu.config';
import { TableColumn } from '../column-setting';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  public tableDataSource = new MatTableDataSource<any>([]);
  public displayedColumns: string[] = [];
  @Input() isPageable = false;
  @Input() tableColumns: TableColumn[] = [];
  @Input() actions?: PopupMenuConfig[] | null;

  @Input() set tableData(data: any[]) {
    this.setTableDataSource(data);
  }

  constructor() {}

  ngOnInit(): void {
    const columnNames = this.tableColumns.map(
      (tableColumn: TableColumn) => tableColumn.name
    );

    this.displayedColumns = columnNames;

    console.log(this.actions);
    if (
      this.actions != null &&
      this.actions!.length > 0 &&
      !this.isAllActionsHidden()
    )
      this.displayedColumns.push('actions');
  }

  setTableDataSource(data: any) {
    this.tableDataSource = new MatTableDataSource<any>(data);
  }

  private isAllActionsHidden(): boolean {
    return this.actions!.every((action: PopupMenuConfig) => action.isHidden);
  }
}

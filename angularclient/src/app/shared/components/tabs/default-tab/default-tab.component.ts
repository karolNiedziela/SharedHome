import { TabConfig } from './tab.config';
import { Component, Input, OnInit } from '@angular/core';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-default-tab',
  templateUrl: './default-tab.component.html',
  styleUrls: ['./default-tab.component.scss'],
})
export class DefaultTabComponent implements OnInit {
  @Input() tabConfig!: TabConfig;

  constructor() {}

  ngOnInit(): void {
    if (!this.isTabConfigValid()) {
      throw new Error('Too many active items.');
    }
  }

  onTabSelected(index: number): void {
    this.tabConfig.tabItems.map((tabItem) => (tabItem.isActive = false));

    this.tabConfig.tabItems[index].isActive = true;
    this.tabConfig.tabItems[index].onClick();
  }

  private isTabConfigValid(): boolean {
    return (
      this.tabConfig.tabItems.filter((tabItem) => tabItem?.isActive).length == 1
    );
  }

  // private findActiveIndex(): number {
  //   return this.tabConfig.tabItems.findIndex((tabItem) => tabItem.isActive);
  // }
}

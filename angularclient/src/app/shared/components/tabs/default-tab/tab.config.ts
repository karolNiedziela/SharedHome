export interface TabConfig {
  tabItems: TabItem[];
}

export interface TabItem {
  text: string;
  onClick(): any;
  isActive?: boolean;
}

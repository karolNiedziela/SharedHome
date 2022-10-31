export interface Paged<T> {
  currentPage: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;
  customTotalItems?: number;
  items: T[];
}

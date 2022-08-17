export interface Paged<T> {
  currentPage: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;
  items: T[];
}

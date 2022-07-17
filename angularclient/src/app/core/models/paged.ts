export interface Paged<Type> {
  currentPage: number;
  pageSize: number;
  totalPages: number;
  totalItems: number;
  items: Type[];
}

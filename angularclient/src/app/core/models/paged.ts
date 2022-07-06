export class Paged<Type> {
  currentPage!: number;
  pageSize!: number;
  totalPages!: number;
  totalItems!: number;
  items!: Type[];
}

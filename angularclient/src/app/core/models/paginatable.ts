export interface Paginatable {
  onPrevious(): void;
  onNext(): void;
  goTo(page: number): void;
}

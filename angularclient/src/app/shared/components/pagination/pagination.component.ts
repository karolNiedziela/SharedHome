import { TranslateService } from '@ngx-translate/core';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnChanges {
  @Input() currentPage: number = 0;
  @Input() totalPages: number = 0;

  @Output() nextPage: EventEmitter<number> = new EventEmitter<number>();
  @Output() previousPage: EventEmitter<number> = new EventEmitter<number>();
  @Output() goToPage: EventEmitter<number> = new EventEmitter<number>();

  public pages: number[] = [];

  constructor() {}
  ngOnChanges(changes: SimpleChanges): void {
    if (
      (changes['currentPage'] && changes['currentPage'].currentValue) ||
      (changes['totalPages'] && changes['totalPages'].currentValue)
    ) {
      this.pages = this.getPages(this.totalPages);
    }
  }

  public onGoTo(page: number): void {
    this.goToPage.emit(page);
  }

  public onNext(): void {
    this.nextPage.emit(this.currentPage);
  }

  public onPrevious(): void {
    this.previousPage.emit(this.currentPage);
  }

  private getPages(totalPages: number): number[] {
    if (totalPages >= 7) {
      return [...Array(7).keys()].map((x) => ++x);
    }

    return [...Array(totalPages).keys()].map((x) => ++x);
  }
}

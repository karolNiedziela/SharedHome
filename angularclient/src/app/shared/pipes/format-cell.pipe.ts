import { CellPipeFormat } from '../components/tables/cell-pipe-format';
import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe, DatePipe, PercentPipe } from '@angular/common';

@Pipe({
  name: 'formatCell',
})
export class FormatCellPipe implements PipeTransform {
  constructor(private datePipe: DatePipe) {}

  transform(value: any, format: CellPipeFormat): any {
    if (value === undefined || value === null) {
      return '-';
    }

    if (format == CellPipeFormat.MONEY) {
      return `${value.price} ${value.currency}`;
    }

    if (format == CellPipeFormat.DATE) {
      return this.datePipe.transform(value, 'mediumDate');
    }

    return value;
  }
}

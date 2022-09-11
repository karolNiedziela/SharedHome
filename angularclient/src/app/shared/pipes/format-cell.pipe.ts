import { EnumAsStringPipe } from './enum-as-string.pipe';
import { CellPipeFormat } from '../components/tables/cell-pipe-format';
import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe, DatePipe, PercentPipe } from '@angular/common';

@Pipe({
  name: 'formatCell',
})
export class FormatCellPipe implements PipeTransform {
  constructor(
    private datePipe: DatePipe,
    private enumAsStringPipe: EnumAsStringPipe
  ) {}

  transform(value: any, format: CellPipeFormat, enumType?: object): any {
    if (value === undefined || value === null) {
      return '-';
    }

    if (format == CellPipeFormat.MONEY) {
      return `${value.price} ${value.currency}`;
    }

    if (format == CellPipeFormat.DATE) {
      return this.datePipe.transform(value, 'mediumDate');
    }

    if (format == CellPipeFormat.ENUM) {
      return this.enumAsStringPipe.transform(value, enumType);
    }

    return value;
  }
}

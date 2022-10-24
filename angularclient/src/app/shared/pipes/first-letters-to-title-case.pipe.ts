import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'firstLettersToTitleCase',
})
export class FirstLettersToTitleCasePipe implements PipeTransform {
  transform(value?: string): string {
    if (!value) {
      return '';
    }

    return value
      .split(' ')
      .map((word: string) => word[0].toUpperCase())
      .join('');
  }
}

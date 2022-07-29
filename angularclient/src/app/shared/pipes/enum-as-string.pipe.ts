import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumAsString',
})
export class EnumAsStringPipe implements PipeTransform {
  transform(value: number | undefined, enumType: any): any {
    if (!value) {
      return;
    }

    return enumType[value];
  }
}

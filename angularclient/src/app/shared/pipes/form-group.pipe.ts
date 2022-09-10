import { Pipe, PipeTransform } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Pipe({
  name: 'formGroup',
})
export class FormGroupPipe implements PipeTransform {
  transform(control: AbstractControl<any, any> | null): any {
    return control as FormGroup;
  }
}

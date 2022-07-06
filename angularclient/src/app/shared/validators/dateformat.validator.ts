import { ValidatorError } from './../../core/models/validator-error';
import { AbstractControl } from '@angular/forms';

export function yearAndMonthFormat(control: AbstractControl): any | null {
  const validSingleDigitMonths = Array.from({ length: 9 }, (_, i) => i + 1);
  const validTwoDigitMonths = [10, 11, 12];
  const validatorError: ValidatorError = {
    invalidFormat: 'Invalid format must be year month, for example 2022 6.',
  };

  const yearAndMonthSplitted = control.value.split(' ');
  if (yearAndMonthSplitted.length != 2) {
    return validatorError;
  }

  if (
    Number.isNaN(yearAndMonthSplitted[0]) ||
    Number.isNaN(Number(yearAndMonthSplitted[1]))
  ) {
    return validatorError;
  }

  if (yearAndMonthSplitted[0].length != 4) {
    return validatorError;
  }

  if (
    yearAndMonthSplitted[1].length != 1 &&
    yearAndMonthSplitted[1].length != 2
  ) {
    return validatorError;
  }

  if (
    yearAndMonthSplitted[1].length == 1 &&
    !validSingleDigitMonths.includes(Number(yearAndMonthSplitted[1]))
  ) {
    return validatorError;
  }

  if (
    yearAndMonthSplitted[1].length == 2 &&
    !validTwoDigitMonths.includes(Number(yearAndMonthSplitted[1]))
  ) {
    return validatorError;
  }

  return null;
}

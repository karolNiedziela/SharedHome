import { AbstractControl } from '@angular/forms';
import { ValidatorError } from './validator-error';

export function passwordStrengthValidator(
  control: AbstractControl
): ValidatorError | null {
  // 6 characters and 1 lowercase
  const passwordRegexp: RegExp = /^.(?=.{6,}$)(?=.*[a-z])/;
  return passwordRegexp.test(control.value)
    ? null
    : {
        invalidFormat: 'shared.inputs.password_invalid_format',
      };
}

export function matchPassword(control: AbstractControl) {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;

  if (password !== confirmPassword) {
    return {
      missmatch: true,
    };
  }

  return null;
}

import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';

export function passwordStrengthValidator(
  control: AbstractControl
): any | null {
  // 6 characters and 1 lowercase
  const passwordRegexp: RegExp = /^.(?=.{6,}$)(?=.*[a-z])/;
  return passwordRegexp.test(control.value)
    ? null
    : {
        invalidFormat: {
          message:
            'Password requires must contain at least 6 characters and one lowercase.',
        },
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

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

export function passwordMatch(
  controlName: string,
  matchingControlName: string
) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];
    if (
      matchingControl.errors &&
      !matchingControl.errors['confirmedValidator']
    ) {
      return;
    }
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ confirmedValidator: true });
    } else {
      matchingControl.setErrors(null);
    }
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

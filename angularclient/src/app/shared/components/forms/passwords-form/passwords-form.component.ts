import { Component, forwardRef, OnDestroy, OnInit, Self } from '@angular/core';
import {
  ControlValueAccessor,
  UntypedFormControl,
  UntypedFormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import {
  matchPassword,
  passwordStrengthValidator,
} from 'app/shared/validators/password.validator';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-passwords-form',
  templateUrl: './passwords-form.component.html',
  styleUrls: ['../../../styles/input.scss', './passwords-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordsFormComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => PasswordsFormComponent),
      multi: true,
    },
  ],
})
export class PasswordsFormComponent implements ControlValueAccessor, OnDestroy {
  passwordForm!: UntypedFormGroup;
  subscriptions: Subscription[] = [];
  eyeSlashIcon: any = faEyeSlash;
  eyeIcon: any = faEye;
  passwordTextField: boolean = false;
  confirmPasswordTextField: boolean = false;

  constructor() {
    this.passwordForm = new UntypedFormGroup(
      {
        password: new UntypedFormControl('', [
          Validators.required,
          passwordStrengthValidator,
        ]),
        confirmPassword: new UntypedFormControl('', [
          Validators.required,
          passwordStrengthValidator,
        ]),
      },
      { validators: matchPassword }
    );

    this.subscriptions.push(
      this.passwordForm.valueChanges.subscribe((value) => {
        this.onChanged(value);
        this.onTouched();
      })
    );
  }
  get value(): any {
    return this.passwordForm.value;
  }

  set value(value: any) {
    this.passwordForm.setValue(value);
    this.onChanged(value);
    this.onTouched();
  }

  get passwordControl() {
    return this.passwordForm.controls['password'];
  }

  get confirmPasswordControl() {
    return this.passwordForm.controls['confirmPassword'];
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((s) => s.unsubscribe());
  }

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  writeValue(value: any): void {
    if (value) {
      this.value = value;
    }

    if (value === null) {
      this.passwordForm.reset();
    }
  }
  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  togglePassword(): void {
    this.passwordTextField = !this.passwordTextField;
  }

  toggleConfirmPassword(): void {
    this.confirmPasswordTextField = !this.confirmPasswordTextField;
  }

  validate(_: UntypedFormControl) {
    return this.passwordForm.valid ? null : { passwords: { valid: false } };
  }

  reset() {
    this.passwordForm.reset();
  }
}

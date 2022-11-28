import { Component, forwardRef, Input, OnDestroy } from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators,
  FormGroup,
  FormControl,
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
  styleUrls: ['../../inputs/input.scss', './passwords-form.component.scss'],
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
  @Input() passwordLabel: string = 'Password';
  @Input() confirmPasswordLabel: string = 'Confirm password';

  passwordForm!: FormGroup;
  subscriptions: Subscription[] = [];
  eyeSlashIcon: any = faEyeSlash;
  eyeIcon: any = faEye;
  passwordTextField: boolean = false;
  confirmPasswordTextField: boolean = false;

  constructor() {
    this.passwordForm = new FormGroup(
      {
        password: new FormControl('', [
          Validators.required,
          passwordStrengthValidator,
        ]),
        confirmPassword: new FormControl('', [
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

  validate(_: FormControl) {
    return this.passwordForm.valid ? null : { passwords: { valid: false } };
  }
}

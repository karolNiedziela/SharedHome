import { Component, forwardRef, Input, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  matchPassword,
  passwordStrengthValidator,
} from '../../validators/password.validator';

@Component({
  selector: 'app-password-form',
  templateUrl: './password-form.component.html',
  styleUrls: ['./password-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordFormComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => PasswordFormComponent),
      multi: true,
    },
  ],
})
export class PasswordFormComponent implements OnInit {
  @Input() passwordLabel: string = 'Password';
  @Input() confirmPasswordLabel: string = 'Confirm password';

  passwordForm!: FormGroup;
  subscriptions: Subscription[] = [];
  passwordTextField: boolean = false;
  confirmPasswordTextField: boolean = false;

  ngOnInit(): void {
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

  get passwordControl(): FormControl {
    return this.passwordForm.controls['password'] as FormControl;
  }

  get confirmPasswordControl(): FormControl {
    return this.passwordForm.controls['confirmPassword'] as FormControl;
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

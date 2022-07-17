import { ControlValueAccessor, NgControl, ValidatorFn } from '@angular/forms';
import { Component, Input, OnInit, Self, AfterViewInit } from '@angular/core';
import { passwordStrengthValidator } from 'app/shared/validators/password.validator';
import { faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
  styleUrls: ['../input.scss', './password-input.component.scss'],
})
export class PasswordInputComponent implements OnInit, ControlValueAccessor {
  @Input() placeholder: string = 'Password';
  @Input() labelText: string = 'Password';
  eyeSlashIcon: any = faEyeSlash;
  eyeIcon: any = faEye;
  fieldTextType: boolean = false;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  constructor(@Self() public controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    let validators: ValidatorFn[] = control?.validator
      ? [control.validator]
      : [];

    validators.push(passwordStrengthValidator);
    control?.setValidators(validators);
    control?.updateValueAndValidity();
  }

  writeValue(value: string): void {
    this.controlDir.control?.setValue(value);
  }
  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  togglePassword(): void {
    this.fieldTextType = !this.fieldTextType;
  }
}

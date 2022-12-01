import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
} from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  Self,
  AfterViewInit,
  Optional,
} from '@angular/core';
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

  get control(): AbstractControl<any, any> | null {
    return this.controlDir.control;
  }

  constructor(@Self() @Optional() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    validators.push(passwordStrengthValidator);
    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  writeValue(value: string): void {
    if (this.control && this.control?.value != value) {
      this.controlDir.control?.setValue(value);
    }
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

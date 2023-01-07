import { Component, Input, OnInit, Self } from '@angular/core';
import {
  AbstractControl,
  UntypedFormControl,
  NgControl,
  ValidatorFn,
  Validators,
  FormControl,
} from '@angular/forms';
import { passwordStrengthValidator } from 'src/app/shared/validators/password.validator';

@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
  styleUrls: ['../input.scss', './password-input.component.scss'],
})
export class PasswordInputComponent implements OnInit {
  @Input() label: string = 'label';
  @Input() placeholder: string = 'placeholder';

  disabled: boolean = false;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): FormControl | null {
    return this.controlDir.control as FormControl;
  }

  get isRequired(): boolean {
    if (this.control?.validator) {
      const validator = this.control?.validator({} as AbstractControl)!;
      if (validator && validator['required']) {
        return true;
      }
    }

    return false;
  }

  constructor(@Self() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {}

  writeValue(value: any): void {
    if (this.control && this.control?.value != value) {
      this.controlDir.control?.setValue(value, { emitEvent: false });
    }
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}

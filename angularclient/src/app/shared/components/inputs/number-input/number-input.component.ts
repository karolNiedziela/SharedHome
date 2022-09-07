import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  Optional,
  Self,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
  ValidatorFn,
} from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['../input.scss', './number-input.component.scss'],
})
export class NumberInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() labelText: string = 'label';
  @Input() placeholder: string = 'placeholder';
  disabled!: boolean;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl<any, any> | null {
    return this.controlDir.control;
  }

  get isRequired(): boolean {
    if (this.control?.validator) {
      return this.control.validator!({} as AbstractControl)!['required'];
    }

    return false;
  }

  constructor(@Self() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  ngOnDestroy(): void {
    this.control?.clearValidators();
    this.control?.markAsPristine();
  }

  writeValue(value: any): void {
    if (this.control && this.control?.value != value) {
      this.control?.setValue(value, { emitEvent: true });
      return;
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

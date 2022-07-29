import { Component, Input, OnDestroy, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['../input.scss'],
})
export class NumberInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() labelText: string = 'label';
  @Input() placeholder: string = 'placeholder';
  @Input() value: number = 0;
  @Input() isRequired: boolean = false;
  disabled!: boolean;

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

    this.value = control?.value;

    control?.setValidators(validators);
    control?.updateValueAndValidity();
  }

  ngOnDestroy(): void {
    this.controlDir.control?.clearValidators();
    this.controlDir.control?.markAsPristine();
  }

  writeValue(value: any): void {
    if (this.controlDir.control && this.controlDir.control?.value != value) {
      this.controlDir.control?.setValue(value, { emitEvent: true });
    }
    this.value = this.controlDir.control?.value;
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

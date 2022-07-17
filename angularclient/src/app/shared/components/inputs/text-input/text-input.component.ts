import { ControlValueAccessor, ValidatorFn, NgControl } from '@angular/forms';
import { Component, Input, OnInit, Self, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['../input.scss'],
})
export class TextInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() labelText: string = 'label';
  @Input() placeholder: string = 'placeholder';
  @Input() value: string = '';
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

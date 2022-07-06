import { ControlValueAccessor, ValidatorFn, NgControl } from '@angular/forms';
import { Component, Input, OnInit, Self } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['../../../styles/input.scss'],
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
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

    control?.setValidators(validators);
    control?.updateValueAndValidity();
  }

  writeValue(value: any): void {
    this.controlDir.control?.setValue(value, { emitEvent: false });
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

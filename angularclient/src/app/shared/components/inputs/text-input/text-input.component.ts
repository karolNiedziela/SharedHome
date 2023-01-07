import {
  Component,
  Input,
  OnChanges,
  OnInit,
  Optional,
  Self,
  SimpleChanges,
} from '@angular/core';
import {
  AbstractControl,
  NgControl,
  ValidatorFn,
  ControlValueAccessor,
} from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['../input.scss', 'text-input.component.scss'],
})
export class TextInputComponent
  implements OnInit, OnChanges, ControlValueAccessor
{
  @Input() label: string = 'label';
  @Input() placeholder: string = 'placeholder';

  disabled: boolean = false;

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl | null {
    return this.controlDir.control!;
  }

  ngOnChanges(changes: SimpleChanges) {
    console.log(changes);
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

  constructor(@Self() @Optional() private controlDir: NgControl) {
    if (this.controlDir != null) {
      this.controlDir.valueAccessor = this;
    }
  }

  ngOnInit(): void {
    let validators: ValidatorFn[] = this.control?.validator
      ? [this.control.validator]
      : [];

    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
  }

  writeValue(value: any): void {
    if (this.controlDir.control && this.controlDir.control?.value != value) {
      this.controlDir.control?.setValue(value);
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

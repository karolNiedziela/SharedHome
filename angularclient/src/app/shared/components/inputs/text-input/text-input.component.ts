import {
  ControlValueAccessor,
  ValidatorFn,
  NgControl,
  AbstractControl,
} from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  Self,
  OnDestroy,
  Optional,
} from '@angular/core';

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
  @Input() isRequired: boolean = false;

  disabled!: boolean;

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

    this.value = this.control?.value;

    this.control?.setValidators(validators);
    this.control?.updateValueAndValidity();
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

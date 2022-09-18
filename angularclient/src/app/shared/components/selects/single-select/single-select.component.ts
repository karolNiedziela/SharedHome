import { SelectSetting } from './select-setting';
import {
  Component,
  Input,
  OnInit,
  Optional,
  Output,
  Self,
  EventEmitter,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
} from '@angular/forms';

@Component({
  selector: 'app-single-select',
  templateUrl: './single-select.component.html',
  styleUrls: ['./single-select.component.scss'],
})
export class SingleSelectComponent implements OnInit, ControlValueAccessor {
  @Input() labelText!: string;
  @Input() selectOptions: SelectSetting[] = [];
  @Output() selectedKey: EventEmitter<string | number> = new EventEmitter<
    string | number
  >();
  selectedValue!: string | number;

  onChanged: (value: string | number) => void = () => {};
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

  constructor(@Self() @Optional() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {}

  writeValue(value: string | number): void {
    this.setValue(value);
  }

  registerOnChange(onChanged: (value: string | number) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setValue(selectedKey: string | number) {
    const selectedValue = this.selectOptions.find(
      (s) => s.key == selectedKey
    )!.value;
    this.selectedValue = selectedValue;

    this.onChanged(selectedValue);
    this.onTouched();

    this.selectedKey.emit(selectedKey);
  }
}

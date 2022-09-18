import {
  Component,
  Input,
  OnInit,
  Output,
  EventEmitter,
  Optional,
  Self,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  NgControl,
} from '@angular/forms';
import { getEnumKeys } from 'app/core/utils/enum.utils';

@Component({
  selector: 'app-enum-select',
  templateUrl: './enum-select.component.html',
  styleUrls: ['../select.scss', './enum-select.component.scss'],
})
export class EnumSelectComponent implements OnInit, ControlValueAccessor {
  @Input() labelText!: string;
  @Input() enumType!: any;
  @Input() firstValueSelected: boolean = false;
  @Output() selectedChanged: EventEmitter<any> = new EventEmitter<any>();
  selectedValue!: number;
  keys: string[] = [];

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

  constructor(@Self() @Optional() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    this.keys = getEnumKeys(this.enumType);

    if (this.firstValueSelected) {
      this.setValue(this.keys[0]);
    }
  }

  writeValue(value: any): void {
    if (this.firstValueSelected && value == null) {
      this.setValue(this.keys[0]);
      return;
    }
    this.selectedValue = value;
  }

  registerOnChange(onChanged: (value: any) => void): void {
    this.onChanged = onChanged;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setValue(selectedValue: string) {
    this.selectedValue = this.enumType[selectedValue];

    this.onChanged(this.selectedValue);
    this.onTouched();

    this.selectedChanged.emit(this.selectedValue);
  }
}
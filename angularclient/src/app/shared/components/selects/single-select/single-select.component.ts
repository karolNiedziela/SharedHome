import { Component, forwardRef, Input, OnInit, OnDestroy } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { getEnumKeys } from 'app/core/utils/enum.utils';

@Component({
  selector: 'app-single-select',
  templateUrl: './single-select.component.html',
  styleUrls: ['../select.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SingleSelectComponent),
      multi: true,
    },
  ],
})
export class SingleSelectComponent implements OnInit, ControlValueAccessor {
  @Input() labelText!: string;
  @Input() enumType!: any;
  selectedValue!: number;
  keys: string[] = [];

  constructor() {}

  onChanged: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  ngOnInit(): void {
    this.keys = getEnumKeys(this.enumType);
  }

  writeValue(value: any): void {
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
  }
}

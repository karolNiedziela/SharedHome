import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Self,
} from '@angular/core';
import { AbstractControl, NgControl } from '@angular/forms';
import { getEnumKeys } from 'src/app/shared/helpers/enum.helper';

@Component({
  selector: 'app-single-enum-select',
  templateUrl: './single-enum-select.component.html',
  styleUrls: ['./single-enum-select.component.scss', '../select.scss'],
})
export class SingleEnumSelectComponent implements OnInit {
  @Input() label!: string;
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

  constructor(@Self() private controlDir: NgControl) {
    controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    this.keys = getEnumKeys(this.enumType);

    if (this.firstValueSelected) {
      this.setValue(this.keys[0]);
    }
  }

  writeValue(value: any): void {
    if (this.firstValueSelected) {
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

import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Self,
} from '@angular/core';
import { AbstractControl, NgControl } from '@angular/forms';
import { SingleSelectSetting } from '../single-select-setting';

@Component({
  selector: 'app-single-select',
  templateUrl: './single-select.component.html',
  styleUrls: ['./single-select.component.scss'],
})
export class SingleSelectComponent implements OnInit {
  @Input() label!: string;
  @Input() selectOptions: SingleSelectSetting[] = [];
  @Output() selectedKey: EventEmitter<string | number> = new EventEmitter<
    string | number
  >();
  selectedValue!: string | number;
  onChanged: (value: string | number) => void = () => {};
  onTouched: () => void = () => {};

  get control(): AbstractControl | null {
    return this.controlDir.control;
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

  constructor(@Self() private controlDir: NgControl) {
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

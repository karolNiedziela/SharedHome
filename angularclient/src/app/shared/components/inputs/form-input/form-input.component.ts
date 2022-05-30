import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
})
export class FormInputComponent {
  @Input() labelText: string = 'label';
  @Input() type: string = 'text';
  @Input() placeholder: string = 'placeholder';
  @Input() isRequired: boolean = false;

  constructor() {}
}

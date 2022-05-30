import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() text: string = '';
  @Input() type: string = 'button';
  @Output() onButtonClick: EventEmitter<any> = new EventEmitter();
  @Input() isDisabled: boolean = false;

  constructor() {}

  onClick(): void {
    this.onButtonClick?.emit(null);
  }
}

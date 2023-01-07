import { ViewEncapsulation } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent implements OnInit {
  @Input() text: string = '';
  @Input() type: string = 'button';
  @Input() loading: boolean = false;
  @Input() disabled: boolean = false;
  @Input() fullWidth: boolean = false;
  @Input() hidden: boolean = false;

  @Output() onButtonClick: EventEmitter<any> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick(): void {
    this.onButtonClick?.emit(null);
  }
}

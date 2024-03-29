import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-circle-button',
  templateUrl: './circle-button.component.html',
  styleUrls: ['./circle-button.component.scss'],
})
export class CircleButtonComponent implements OnInit {
  @Input() disabled: boolean = false;
  @Input() icon: string = 'add';
  @Input() tooltip: string = 'Tooltip text';
  @Output() onButtonClick: EventEmitter<any> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick(): void {
    this.onButtonClick?.emit(null);
  }
}

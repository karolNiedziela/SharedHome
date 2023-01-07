import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-circle-button',
  templateUrl: './circle-button.component.html',
  styleUrls: ['./circle-button.component.scss'],
})
export class CircleButtonComponent implements OnInit {
  @Input() disabled: boolean = false;
  @Input() icon: string = 'add';
  @Input() tooltipText: string = 'Tooltip text';
  @Output() onButtonClick: EventEmitter<any> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick(): void {
    this.onButtonClick?.emit(null);
  }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-circle-button',
  templateUrl: './circle-button.component.html',
  styleUrls: ['./circle-button.component.scss'],
})
export class CircleButtonComponent implements OnInit {
  @Output() onButtonClick: EventEmitter<any> = new EventEmitter();
  @Input() isDisabled: boolean = false;
  @Input() icon: any = faPlus;
  @Input() tooltipText: string = 'Tooltip text';

  constructor() {}

  ngOnInit(): void {}

  onClick(): void {
    this.onButtonClick?.emit(null);
  }
}

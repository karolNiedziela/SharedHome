import { FormControl, FormGroup } from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  OnDestroy,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { NetContentType } from '../../enums/net-content-type';

@Component({
  selector: 'app-net-content-form',
  templateUrl: './net-content-form.component.html',
  styleUrls: ['./net-content-form.component.scss'],
})
export class NetContentFormComponent implements OnInit, OnDestroy {
  @Input() parentFormGroup!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor() {}

  ngOnInit(): void {
    this.parentFormGroup.addControl('netContent', new FormControl(''));

    this.parentFormGroup.addControl('netContentType', new FormControl(''));
  }

  ngOnDestroy(): void {
    this.parentFormGroup.reset();
  }
}

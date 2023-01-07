import { FormControl, FormGroup } from '@angular/forms';
import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { NetContentType } from '../../enums/net-content-type';

@Component({
  selector: 'app-net-content-form',
  templateUrl: './net-content-form.component.html',
  styleUrls: ['./net-content-form.component.scss'],
})
export class NetContentFormComponent implements OnInit, OnDestroy {
  @Input() netContentFormGroup!: FormGroup;

  public netContentType: typeof NetContentType = NetContentType;

  constructor() {}

  ngOnInit(): void {
    this.netContentFormGroup.addControl('netContent', new FormControl(null));

    this.netContentFormGroup.addControl(
      'netContentType',
      new FormControl(null)
    );
  }

  ngOnDestroy(): void {
    this.netContentFormGroup.reset();
  }
}

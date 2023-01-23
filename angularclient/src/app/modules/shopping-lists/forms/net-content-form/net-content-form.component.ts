import { FormControl, FormGroup } from '@angular/forms';
import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { NetContentType } from '../../enums/net-content-type';
import { NetContent } from '../../models/net-content';

@Component({
  selector: 'app-net-content-form',
  templateUrl: './net-content-form.component.html',
  styleUrls: ['./net-content-form.component.scss'],
})
export class NetContentFormComponent implements OnInit, OnDestroy {
  @Input() netContentFormGroup!: FormGroup;

  get netContentControl() {
    return this.netContentFormGroup.controls['netContent'];
  }

  get netContentTypeControl() {
    return this.netContentFormGroup.controls['netContentType'];
  }

  get money(): NetContent {
    return {
      netContent: this.netContentControl?.value,
      netContentType: this.netContentTypeControl?.value,
    };
  }

  public netContentType: typeof NetContentType = NetContentType;

  constructor() {}

  ngOnInit(): void {
    this.netContentFormGroup.addControl('netContent', new FormControl(''));

    this.netContentFormGroup.addControl('netContentType', new FormControl(''));
  }

  ngOnDestroy(): void {
    this.netContentFormGroup.reset();
  }
}

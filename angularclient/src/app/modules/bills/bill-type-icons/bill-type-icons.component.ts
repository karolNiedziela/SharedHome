import { BillEvent } from './../models/bill-event';
import {
  faBolt,
  faDroplet,
  faGasPump,
  faHouse,
  faPhone,
  faQuestion,
  faTrash,
  faWifi,
} from '@fortawesome/free-solid-svg-icons';
import { BillType } from './../enums/bill-type';
import { Component, Input, OnInit } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

@Component({
  selector: 'app-bill-type-icons',
  templateUrl: './bill-type-icons.component.html',
  styleUrls: ['./bill-type-icons.component.scss'],
})
export class BillTypeIconsComponent implements OnInit {
  @Input() billTypes: BillType[] = [];
  @Input() billEvents: BillEvent[] = [];

  public billTypeType: typeof BillType = BillType;

  iconByBillType: Record<string, IconProp> = {
    [BillType.Other]: faQuestion,
    [BillType.Rent]: faHouse,
    [BillType.Gas]: faGasPump,
    [BillType.Electricity]: faBolt,
    [BillType.Trash]: faTrash,
    [BillType.Phone]: faPhone,
    [BillType.Internet]: faWifi,
    [BillType.Water]: faDroplet,
  };

  constructor() {}

  ngOnInit(): void {}
}

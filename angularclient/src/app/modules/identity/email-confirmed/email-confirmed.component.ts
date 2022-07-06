import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-email-confirmed',
  templateUrl: './email-confirmed.component.html',
  styleUrls: ['./email-confirmed.component.scss', '../identity.component.scss'],
})
export class EmailConfirmedComponent implements OnInit {
  confirmedMessage: string = 'Email adress confirmed.';

  constructor() {}

  ngOnInit(): void {}
}

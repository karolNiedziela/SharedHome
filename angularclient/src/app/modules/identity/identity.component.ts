import { LoadingService } from './../../core/services/loading.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.scss'],
})
export class IdentityComponent implements OnInit {
  constructor(public loadingService: LoadingService) {}

  ngOnInit(): void {}
}

import { CurrentUser } from './../models/current-user';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  public currentUser: CurrentUser;

  constructor() {
    this.currentUser = {
      defaultCurrency: 'zł',
    };
  }
}

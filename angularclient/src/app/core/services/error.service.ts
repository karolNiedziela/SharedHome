import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  private _errors: BehaviorSubject<string[]> = new BehaviorSubject<string[]>(
    []
  );
  public errors$: Observable<string[]> = this._errors.asObservable();
  constructor() {}

  propagateErrors(errors: string[]) {
    this._errors.next(errors);
  }

  clearErrors() {
    this._errors.next([]);
  }
}

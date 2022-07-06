import { ShoppingListMonthlyCost } from './../models/shopping-list-monthly-cost';
import { ShoppingList } from './../models/shopping-list';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShoppingListsService {
  private shoppingListsUrl: string = `${environment.apiUrl}/shoppinglists`;

  constructor(private http: HttpClient) {}

  get(shoppingListId: number): Observable<ShoppingList> {
    return this.http.get<ShoppingList>(
      `${this.shoppingListsUrl}/${shoppingListId}`
    );
  }

  getAllByYearAndMonthAndIsDone(
    year: number,
    month: number,
    isDone: boolean
  ): Observable<ShoppingList[]> {
    let params = new HttpParams();
    params = params.append('year', year);
    params = params.append('month', month);
    params = params.append('isdone', isDone);

    return this.http.get<ShoppingList[]>(this.shoppingListsUrl, {
      params: params,
    });
  }

  getMonthlyCostByYear(year: number): Observable<ShoppingListMonthlyCost[]> {
    return this.http.get<ShoppingListMonthlyCost[]>(this.shoppingListsUrl, {
      params: new HttpParams().set('year', year),
    });
  }
}

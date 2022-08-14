import { CancelPurchaseOfProduct } from './../models/cancel-purchase-of-product';
import { PurchaseShoppingListProduct } from './../models/purchase-shopping-list-product';
import { AddShoppingList } from './../models/add-shopping-list';
import { ShoppingListMonthlyCost } from './../models/shopping-list-monthly-cost';
import { ShoppingList } from './../models/shopping-list';
import { Observable, map, Subject, BehaviorSubject, tap } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { AddShoppingListProduct } from '../models/add-shopping-list-product';
import { ChangePriceOfProduct } from '../models/change-price-of-product';
import { UpdateShoppingList } from '../models/update-shopping-list';
import { Paged } from 'app/core/models/paged';
import { ApiResponse } from 'app/core/models/api-response';
import { MarkAsDone } from '../models/mark-as-done';

@Injectable({
  providedIn: 'root',
})
export class ShoppingListsService {
  private shoppingListsUrl: string = `${environment.apiUrl}/shoppinglists`;

  private _allShoppingListRefreshNeeded = new Subject<void>();
  private _singleShoppingListRefreshNeeded = new Subject<void>();

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  get allShoppingListRefreshNeeded() {
    return this._allShoppingListRefreshNeeded;
  }

  get singleShoppingListRefreshNeeded() {
    return this._singleShoppingListRefreshNeeded;
  }

  constructor(private http: HttpClient) {}

  get(shoppingListId: number): Observable<ApiResponse<ShoppingList>> {
    return this.http.get<ApiResponse<ShoppingList>>(
      `${this.shoppingListsUrl}/${shoppingListId}`
    );
  }

  getAllByYearAndMonthAndIsDone(
    year: number,
    month: number,
    isDone: boolean
  ): Observable<Paged<ShoppingList>> {
    let params = new HttpParams();
    params = params.append('year', year);
    params = params.append('month', month);
    params = params.append('isdone', isDone);

    return this.http.get<Paged<ShoppingList>>(this.shoppingListsUrl, {
      params: params,
    });
  }

  getMonthlyCostByYear(year: number): Observable<ShoppingListMonthlyCost[]> {
    return this.http.get<ShoppingListMonthlyCost[]>(this.shoppingListsUrl, {
      params: new HttpParams().set('year', year),
    });
  }

  add(shoppingList: AddShoppingList): Observable<ShoppingList> {
    return this.http
      .post<ShoppingList>(
        this.shoppingListsUrl,
        shoppingList,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allShoppingListRefreshNeeded.next();
        })
      );
  }

  addShoppingListProduct(
    addShoppingListProducts: AddShoppingListProduct
  ): Observable<any> {
    return this.http
      .put<any>(
        `${this.shoppingListsUrl}/${addShoppingListProducts.shoppingListId}/products`,
        addShoppingListProducts,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }

  purchaseProduct(
    purchaseProduct: PurchaseShoppingListProduct
  ): Observable<any> {
    return this.http
      .patch<any>(
        `${this.shoppingListsUrl}/${purchaseProduct.shoppingListId}/products/${purchaseProduct.productName}/purchase`,
        purchaseProduct,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }

  cancelPurchaseOfProduct(
    cancelPurchase: CancelPurchaseOfProduct
  ): Observable<any> {
    return this.http
      .patch<any>(
        `${this.shoppingListsUrl}/${cancelPurchase.shoppingListId}/products/${cancelPurchase.productName}/cancelpurchase`,
        cancelPurchase,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }

  changePriceOfProduct(
    changePriceOfProduct: ChangePriceOfProduct
  ): Observable<any> {
    return this.http.patch<any>(
      `${this.shoppingListsUrl}/${changePriceOfProduct.shoppingListId}/products/${changePriceOfProduct.productName}/changeprice`,
      changePriceOfProduct,
      this.defaultHttpOptions
    );
  }

  markAsDone(
    markAsDone: MarkAsDone,
    isSingleRefresh: boolean
  ): Observable<any> {
    return this.http
      .patch<any>(
        `${this.shoppingListsUrl}/${markAsDone.shoppingListId}/setisdone`,
        markAsDone,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          isSingleRefresh
            ? this._singleShoppingListRefreshNeeded.next()
            : this._allShoppingListRefreshNeeded.next();
        })
      );
  }

  update(shoppingList: UpdateShoppingList): Observable<any> {
    return this.http.put<any>(
      this.shoppingListsUrl,
      shoppingList,
      this.defaultHttpOptions
    );
  }

  delete(shoppingListId: number): Observable<any> {
    return this.http
      .delete<any>(
        `${this.shoppingListsUrl}/${shoppingListId}`,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allShoppingListRefreshNeeded.next();
        })
      );
  }

  deleteShoppingListProduct(
    shoppingListId: number,
    productName: string
  ): Observable<any> {
    return this.http
      .delete<any>(
        `${this.shoppingListsUrl}/${shoppingListId}/products/${productName}`,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }
}

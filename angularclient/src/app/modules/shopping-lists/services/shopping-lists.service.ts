import { ShoppingListStatus } from './../enums/shopping-list-status';
import { PurchaseShoppingListProducts } from './../models/purchase-shopping-list-products';
import { CancelPurchaseOfProduct } from './../models/cancel-purchase-of-product';
import { PurchaseShoppingListProduct } from './../models/purchase-shopping-list-product';
import { AddShoppingList } from './../models/add-shopping-list';
import { ShoppingListMonthlyCost } from './../models/shopping-list-monthly-cost';
import { ShoppingList } from './../models/shopping-list';
import { Observable, map, Subject, tap } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddShoppingListProduct } from '../models/add-shopping-list-product';
import { ChangePriceOfProduct } from '../models/change-price-of-product';
import { UpdateShoppingList } from '../models/update-shopping-list';
import { MarkAsDone } from '../models/mark-as-done';
import { UpdateShoppingListProduct } from '../models/update-shopping-list-product';
import { environment } from 'src/environments/environment';
import { ApiResponse } from 'src/app/core/models/api-response';
import { Paged } from 'src/app/core/models/paged';

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

  get(shoppingListId: string): Observable<ApiResponse<ShoppingList>> {
    return this.http
      .get<ApiResponse<ShoppingList>>(
        `${this.shoppingListsUrl}/${shoppingListId}`
      )
      .pipe(
        map((response: ApiResponse<ShoppingList>) => {
          const shoppingListResponse: ApiResponse<ShoppingList> = {
            data: new ShoppingList(response.data),
            message: response.message,
          };

          return shoppingListResponse;
        })
      );
  }

  getAllByYearAndMonthAndIsDone(
    year: number,
    month: number,
    status: ShoppingListStatus,
    pageNumber: number
  ): Observable<Paged<ShoppingList>> {
    let params = new HttpParams();
    params = params.append('year', year);
    params = params.append('month', month);
    params = params.append('status', status);
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', 10);

    return this.http
      .get<Paged<ShoppingList>>(this.shoppingListsUrl, {
        params: params,
      })
      .pipe(
        map((response: Paged<ShoppingList>) => {
          const paged: Paged<ShoppingList> = {
            items: response.items.map((item: any) => new ShoppingList(item)),
            totalItems: response.totalItems,
            totalPages: response.totalPages,
            currentPage: response.currentPage,
            pageSize: response.pageSize,
          };

          return paged;
        })
      );
  }

  getMonthlyCostByYear(
    year: number
  ): Observable<ApiResponse<ShoppingListMonthlyCost[]>> {
    let params = new HttpParams();
    params = params.append('year', year);

    return this.http.get<ApiResponse<ShoppingListMonthlyCost[]>>(
      `${this.shoppingListsUrl}/monthlycost`,
      {
        params: params,
      }
    );
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

  addShoppingListProducts(
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

  purchaseProducts(
    purchaseProducts: PurchaseShoppingListProducts
  ): Observable<any> {
    return this.http
      .put<any>(
        `${this.shoppingListsUrl}/${purchaseProducts.shoppingListId}/products/purchase`,
        purchaseProducts,
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
    return this.http
      .patch<any>(
        `${this.shoppingListsUrl}/${changePriceOfProduct.shoppingListId}/products/${changePriceOfProduct.productName}/changeprice`,
        changePriceOfProduct,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
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

  update(
    shoppingList: UpdateShoppingList,
    isSingleRefresh: boolean
  ): Observable<any> {
    return this.http
      .put<any>(this.shoppingListsUrl, shoppingList, this.defaultHttpOptions)
      .pipe(
        tap(() => {
          isSingleRefresh
            ? this._singleShoppingListRefreshNeeded.next()
            : this._allShoppingListRefreshNeeded.next();
        })
      );
  }

  updateShoppingListProduct(
    updateShoppingListProduct: UpdateShoppingListProduct
  ): Observable<any> {
    return this.http
      .put<any>(
        `${this.shoppingListsUrl}/${updateShoppingListProduct.shoppingListId}/products/${updateShoppingListProduct.currentProductName}/update`,
        updateShoppingListProduct,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }

  delete(shoppingListId: string): Observable<any> {
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
    shoppingListId: string,
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

  deleteShoppingListProducts(
    shoppingListId: string,
    productNames: string[]
  ): Observable<any> {
    return this.http
      .delete<any>(`${this.shoppingListsUrl}/${shoppingListId}/products`, {
        headers: this.defaultHttpOptions.headers,
        body: {
          shoppingListId: shoppingListId,
          productNames: productNames,
        },
      })
      .pipe(
        tap(() => {
          this._singleShoppingListRefreshNeeded.next();
        })
      );
  }
}

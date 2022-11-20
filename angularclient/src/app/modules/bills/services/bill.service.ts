import { SettingRoutingModule } from './../../settings/setting-routing.module';
import { BillMonthlyCost } from '../models/bill-monthly-cost';
import { PayForBill } from './../models/pay-for-bill';
import { AddBill } from './../models/add-bill';
import { Bill } from './../models/bill';
import { Observable, Subject, tap } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiResponse } from 'app/core/models/api-response';
import { UpdateBill } from '../models/update-bill';

@Injectable({
  providedIn: 'root',
})
export class BillService {
  private billsUrl: string = `${environment.apiUrl}/bills`;

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  private _allBillsRefreshNeeded = new Subject<void>();

  get allBillsRefreshNeeded() {
    return this._allBillsRefreshNeeded;
  }

  constructor(private httpClient: HttpClient) {}

  getAllByYearAndMonthAndIsDone(
    year: number,
    month: number,
    isPaid?: boolean
  ): Observable<ApiResponse<Bill[]>> {
    let params = new HttpParams();
    params = params.append('year', year);
    params = params.append('month', month);

    return this.httpClient.get<ApiResponse<Bill[]>>(this.billsUrl, {
      params: params,
    });
  }

  getMonthlyCost(year: number): Observable<ApiResponse<BillMonthlyCost[]>> {
    let params = new HttpParams();
    params = params.append('year', year);

    return this.httpClient.get<ApiResponse<BillMonthlyCost[]>>(
      `${this.billsUrl}/monthlycost`,
      {
        params: params,
      }
    );
  }

  addBill(bill: AddBill): Observable<ApiResponse<Bill>> {
    return this.httpClient
      .post<ApiResponse<Bill>>(this.billsUrl, bill, this.defaultHttpOptions)
      .pipe(
        tap(() => {
          this._allBillsRefreshNeeded.next();
        })
      );
  }

  payForBill(payForBill: PayForBill): Observable<any> {
    return this.httpClient
      .patch<any>(
        `${this.billsUrl}/${payForBill.billId}/pay`,
        payForBill,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allBillsRefreshNeeded.next();
        })
      );
  }

  cancelBillPayment(billId: SettingRoutingModule): Observable<any> {
    return this.httpClient
      .patch<any>(
        `${this.billsUrl}/${billId}/cancelpayment`,
        {
          billId: billId,
        },
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allBillsRefreshNeeded.next();
        })
      );
  }

  updateBill(updateBill: UpdateBill): Observable<any> {
    return this.httpClient
      .put<any>(`${this.billsUrl}`, updateBill, this.defaultHttpOptions)
      .pipe(
        tap(() => {
          this._allBillsRefreshNeeded.next();
        })
      );
  }

  deleteBill(billId: string): Observable<any> {
    return this.httpClient
      .delete<any>(`${this.billsUrl}/${billId}`, this.defaultHttpOptions)
      .pipe(
        tap(() => {
          this._allBillsRefreshNeeded.next();
        })
      );
  }
}

import { Bill } from './../models/bill';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiResponse } from 'app/core/models/api-response';

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

  constructor(private httpClient: HttpClient) {}

  getAllByYearAndMonthAndIsDone(
    year: number,
    month: number,
    isPaid?: boolean
  ): Observable<ApiResponse<Bill[]>> {
    let params = new HttpParams();
    params = params.append('year', year);
    params = params.append('month', month);

    const test = this.httpClient.get<ApiResponse<Bill[]>>(this.billsUrl, {
      params: params,
    });
    console.log(test);
    return test;
  }
}

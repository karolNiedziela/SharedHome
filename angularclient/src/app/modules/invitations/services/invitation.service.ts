import { Observable, Subject, tap } from 'rxjs';
import { SendInvitation } from '../models/send-invitation';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Invitation } from '../models/invitation';
import { ApiResponse } from 'app/core/models/api-response';
import { AcceptInvitation } from '../models/accept-invitation';
import { RejectInvitation } from '../models/reject-invitation';

@Injectable({
  providedIn: 'root',
})
export class InvitationService {
  private invitationsUrl: string = `${environment.apiUrl}/invitations`;

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  private _allInvitationsRefreshNeeded = new Subject<void>();

  get allInvitationsRefreshNeeded() {
    return this._allInvitationsRefreshNeeded;
  }

  constructor(private httpClient: HttpClient) {}

  getByStatus(status?: number): Observable<ApiResponse<Invitation[]>> {
    let params = new HttpParams();
    if (status != null) {
      params = params.append('status', status);
    }

    return this.httpClient.get<ApiResponse<Invitation[]>>(
      `${this.invitationsUrl}`,
      {
        params: params,
      }
    );
  }

  send(sendInvitation: SendInvitation): Observable<ApiResponse<Invitation>> {
    return this.httpClient.post<ApiResponse<Invitation>>(
      this.invitationsUrl,
      sendInvitation,
      this.defaultHttpOptions
    );
  }

  accept(acceptInvitation: AcceptInvitation): Observable<any> {
    return this.httpClient
      .patch<any>(
        `${this.invitationsUrl}/accept`,
        acceptInvitation,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allInvitationsRefreshNeeded.next();
        })
      );
  }

  reject(rejectInvitation: RejectInvitation): Observable<any> {
    return this.httpClient
      .patch<any>(
        `${this.invitationsUrl}/reject`,
        rejectInvitation,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._allInvitationsRefreshNeeded.next();
        })
      );
  }

  delete(houseGroupId: string): Observable<any> {
    return this.httpClient
      .delete<any>(`${this.invitationsUrl}/${houseGroupId}`, {
        headers: this.defaultHttpOptions.headers,
        body: {
          houseGroupId: houseGroupId,
        },
      })
      .pipe(
        tap(() => {
          this._allInvitationsRefreshNeeded.next();
        })
      );
  }
}

import { Observable } from 'rxjs';
import { SendInvitation } from '../models/send-invitation';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Invitation } from '../models/invitation';
import { ApiResponse } from 'app/core/models/api-response';

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

  constructor(private httpClient: HttpClient) {}

  getByStatus(status?: number): Observable<ApiResponse<Invitation[]>> {
    let params = new HttpParams();
    console.log(status);
    if (status != null) {
      params = params.append('status', status);
    }

    console.log(params);

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
}

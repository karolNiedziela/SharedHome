import { AddHouseGroup } from './../models/add-house-group';
import { HouseGroup } from './../models/housegroup';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ApiResponse } from 'app/core/models/api-response';
import { RemoveMember } from '../models/remove-member';
import { HandOwnerRoleOver } from '../models/hand-owner-role-over';

@Injectable({
  providedIn: 'root',
})
export class HouseGroupService {
  private houseGroupsUrl: string = `${environment.apiUrl}/housegroups`;

  private defaultHttpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  private _houseGroupRefreshNeeded = new Subject<void>();

  get houseGroupRefreshNeeded() {
    return this._houseGroupRefreshNeeded;
  }
  constructor(private httpClient: HttpClient) {}

  get(): Observable<ApiResponse<HouseGroup>> {
    return this.httpClient.get<ApiResponse<HouseGroup>>(
      this.houseGroupsUrl,
      this.defaultHttpOptions
    );
  }

  add(addHouseGroup: AddHouseGroup): Observable<ApiResponse<HouseGroup>> {
    return this.httpClient
      .post<ApiResponse<HouseGroup>>(
        this.houseGroupsUrl,
        addHouseGroup,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._houseGroupRefreshNeeded.next();
        })
      );
  }

  removeMember(removeMember: RemoveMember): Observable<any> {
    return this.httpClient
      .delete<any>(
        `${this.houseGroupsUrl}/${removeMember.houseGroupId}/members/${removeMember.personToRemoveId}`,
        {
          headers: this.defaultHttpOptions.headers,
          body: removeMember,
        }
      )
      .pipe(
        tap(() => {
          this._houseGroupRefreshNeeded.next();
        })
      );
  }

  handOwnerRoleOver(handOwnerRoleOver: HandOwnerRoleOver): Observable<any> {
    return this.httpClient
      .patch<any>(
        `${this.houseGroupsUrl}/${handOwnerRoleOver.houseGroupId}/handownerroleover`,
        handOwnerRoleOver,
        this.defaultHttpOptions
      )
      .pipe(
        tap(() => {
          this._houseGroupRefreshNeeded.next();
        })
      );
  }

  leaveHouseGroup(houseGroupId: number): Observable<any> {
    return this.httpClient
      .delete<any>(`${this.houseGroupsUrl}/${houseGroupId}/leave`, {
        headers: this.defaultHttpOptions.headers,
        body: {
          houseGroupId: houseGroupId,
        },
      })
      .pipe(
        tap(() => {
          this._houseGroupRefreshNeeded.next();
        })
      );
  }
}

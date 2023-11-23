import { state } from '@angular/animations';

import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })

  export class ColumnStatusService extends BaseApiService{
    constructor(http: HttpClient) {
      super(http);
    }
  
    changeUrl() {
      return 'ColumnStatus';
    }
  
    getAllPaging(request): Observable<any> {
      return this.http.post(this.rootUrl + "/GetAllPaging?", request);
    }
    createColumnStatus(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Create?", request);
    }

    updateColumnStatus(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Edit?", request);
    }

    delete(id: number): Observable<any> {
      return this.http.delete(this.rootUrl + `/Delete?id=${id}`);
    }

    getOneColumnStatus(id: number): Observable<any> {
      return this.http.get(this.rootUrl + `/GetOneColumnStatus?id=${id}`);
    }

    getAllColumnStatusTicketNotPaging(): Observable<any> {
      return this.http.get(this.rootUrl + "/getAllColumnStatusTicketNotPaging");
    }

    getAllColumnStatusTaskNotPaging(): Observable<any> {
      return this.http.get(this.rootUrl + "/getAllColumnStatusTaskNotPaging");
    }
  }
  
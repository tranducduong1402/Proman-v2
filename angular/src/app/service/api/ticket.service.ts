import { state } from '@angular/animations';

import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })

  export class TicketService extends BaseApiService{
    constructor(http: HttpClient) {
      super(http);
    }
  
    changeUrl() {
      return 'ticket';
    }
  
    getAllPaging(request): Observable<any> {
      return this.http.post(this.rootUrl + "/GetAllPaging?", request);
    }
    createTicket(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Create?", request);
    }

    updateTicket(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Edit?", request);
    }

    delete(id: number): Observable<any> {
      return this.http.delete(this.rootUrl + `/Delete?id=${id}`);
    }

    getOneTicket(id: number): Observable<any> {
      return this.http.get(this.rootUrl + `/GetOneTicket?id=${id}`);
    }

    getAllTicketNotPaging(): Observable<any> {
      return this.http.get(this.rootUrl + "/GetAllTicketNotPaging");
    }

  }
  
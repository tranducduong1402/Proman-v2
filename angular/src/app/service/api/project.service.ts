import { state } from '@angular/animations';

import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })

  export class ProjectService extends BaseApiService{
    constructor(http: HttpClient) {
      super(http);
    }
  
    changeUrl() {
      return 'Project';
    }
  
    getAllPaging(request): Observable<any> {
      return this.http.post(this.rootUrl + "/GetAllPaging?", request);
    }
    createProject(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Create?", request);
    }

    delete(id: number): Observable<any> {
      return this.http.delete(this.rootUrl + `/Delete?id=${id}`);
    }

    getOneProject(id: number): Observable<any> {
      return this.http.get(this.rootUrl + `/GetOneProject?id=${id}`);
    }
  }
  
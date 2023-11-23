import { state } from '@angular/animations';

import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })

  export class TaskService extends BaseApiService{
    constructor(http: HttpClient) {
      super(http);
    }
  
    changeUrl() {
      return 'task';
    }
  
    getAllPaging(request): Observable<any> {
      return this.http.post(this.rootUrl + "/GetAllPaging?", request);
    }
    createTask(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Create?", request);
    }

    updateTask(request): Observable<any> {
      return this.http.post(this.rootUrl + "/Edit?", request);
    }

    delete(id: number): Observable<any> {
      return this.http.delete(this.rootUrl + `/Delete?id=${id}`);
    }

    getOneTask(id: number): Observable<any> {
      return this.http.get(this.rootUrl + `/GetOneTask?id=${id}`);
    }

    getAllTaskNotPaging(): Observable<any> {
      return this.http.get(this.rootUrl + "/GetAllTaskNotPaging");
    }

  }
  
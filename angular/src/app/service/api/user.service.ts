
import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })

  export class UserService extends BaseApiService{
    constructor(http: HttpClient) {
      super(http);
    }
  
    changeUrl() {
      return 'User';
    }
  
    getAllUser(request): Observable<any> {
      return this.http.post(this.rootUrl + "/GetAllUserPaging?", request);
    }

    getAllClient(request): Observable<any> {
        return this.http.post(this.rootUrl + "/GetAllClientPaging?", request);
    }

    createClient(request): Observable<any> {
        return this.http.post(this.rootUrl + "/CreateClient?", request);
    }
  }
  
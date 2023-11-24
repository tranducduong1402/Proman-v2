import { state } from "@angular/animations";

import { BaseApiService } from "./base-api.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class CommentService extends BaseApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  changeUrl() {
    return "comment";
  }

  getAll(id): Observable<any> {
    return this.http.get(this.rootUrl + `/getAll?ticketID=${id}`);
  }
  createComment(body): Observable<any> {
    return this.http.post(this.rootUrl + "/Create", body);
  }
  update(body): Observable<any> {
    return this.http.post(this.rootUrl + "/update", body);
  }
  delete(id): Observable<any> {
    return this.http.delete(this.rootUrl + `/delete?id=${id}`);
  }
}

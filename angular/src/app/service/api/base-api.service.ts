//import { PagedRequestDto } from '@shared/paged-listing-component-base';

import { HttpClient, HttpParams } from '@angular/common/http';
import { AppConsts } from './../../../shared/AppConsts';
import { Observable } from 'rxjs';

export abstract class BaseApiService {
    protected baseUrl = AppConsts.remoteServiceBaseUrl;

    protected get rootUrl() {
        return this.baseUrl + '/api/services/app/' + this.changeUrl();
    }

    protected http: HttpClient;
    constructor(http: HttpClient) {
        this.http = http;
    }

    abstract changeUrl();

    protected getUrl(url: string) {        
        return this.rootUrl + '/' + url;
    }

    //
    getOne(id: any, includes?: any): Observable<any> {
        return this.http.get(this.rootUrl + '/Get?' + 'id=${id}');
    }

    // filterAndPaging(request: FilterRequest): Observable<any> {
    //     return this.http.post<any>(this.rootUrl + '/GetAllPaging', request);
    // }

    // getAllPagging(request: PagedRequestDto): Observable<any> {
    //     return this.http.post<any>(this.rootUrl + '/GetAllPaging', request);
    // }

    public getById(id: any): Observable<any> {
        return this.http.get<any>(this.rootUrl + '/Get?id=' + id);
    }

    public delete(id: any): Observable<any> {
        return this.http.delete<any>(this.rootUrl + '/Delete', {
            params: new HttpParams().set('Id', id)
        })
    }

    public update(item: any): Observable<any> {
        return this.http.put<any>(this.rootUrl + '/Update', item);
    }

    public create(item: any): Observable<any> {
        return this.http.post<any>(this.rootUrl + '/Create', item);
    }

    //

   
    // filter(key: FilterRequest): Observable<any> {
    //     return this.http.get(this.rootUrl + '/Filter?' + `Includes=${key.includes}&Filters=${key.filters}&Sorts=${key.sorts}&Page=${key.page}&PageSize=${key.pageSize}`);
    // }
    
    save(data: object): Observable<any> {
        return this.http.post(this.rootUrl + '/Save', data);
    }
    
}
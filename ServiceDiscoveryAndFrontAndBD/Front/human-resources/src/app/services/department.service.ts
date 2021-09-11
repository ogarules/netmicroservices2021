import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PageableService } from './pageable-service';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService implements PageableService {

  totalRecords: number;
  constructor(private httpClient: HttpClient) { }
  
  getPagedData(pageNumber: number, pageSize: number): Observable<any> {
    return this.httpClient.get(`${environment.apiUrl}/department?page=${pageNumber}&pagesize=${pageSize}`)
  }

  getById(id) {
    return this.httpClient.get(`${environment.apiUrl}/department/${id}`)
  }

  add(data) {
    return this.httpClient.post(`${environment.apiUrl}/department`, data);
  }

  update(id, data) {
    return this.httpClient.put(`${environment.apiUrl}/department/${id}`, data);
  }

}
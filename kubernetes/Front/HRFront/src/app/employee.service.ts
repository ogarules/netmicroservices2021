import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PageableService } from './datasources/pageable-service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService implements PageableService {

  constructor(private httpClient : HttpClient) { }
  totalRecords: number;
  public id = 0;

  getPagedData(pageNumber: number, pageSize: number): Observable<any> {
    return this.httpClient.get(`${environment.apiUrl}/employee/department/${this.id}?page=${pageNumber}&pageSize=${pageSize}`);
  }

  getById(id) {
    return this.httpClient.get(`${environment.apiUrl}/employee/${id}`);
  }

  add(data) {
    return this.httpClient.post(`${environment.apiUrl}/employee`, data);
  }

  update(id, data) {
    return this.httpClient.put(`${environment.apiUrl}/employee/${id}`, data);
  }
}

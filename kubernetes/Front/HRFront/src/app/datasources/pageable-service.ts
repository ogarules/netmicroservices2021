import { Observable } from 'rxjs';

export interface PageableService{
    totalRecords : number;

    getPagedData(pageNumber : number, pageSize : number) : Observable<any>;
}
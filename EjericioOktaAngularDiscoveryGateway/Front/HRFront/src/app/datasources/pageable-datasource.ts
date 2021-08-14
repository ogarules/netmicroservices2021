import { DataSource, CollectionViewer } from "@angular/cdk/collections";
import { ElementRef } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { BehaviorSubject, Observable, of, fromEvent } from 'rxjs';
import { catchError, finalize, tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NotificationMessageService } from "./notification-message.service";
import { PageableService } from "./pageable-service";

export class PageableDatasource implements DataSource<any> {
    public totalRecords : number = 0;
    private dataSubject = new BehaviorSubject<any[]>([]);
    private loadingData = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingData.asObservable();

    constructor(private dataService : PageableService, private notificationService:NotificationMessageService) {       
    }

    connect(collectionViewer: CollectionViewer): Observable<any[]> {
        return this.dataSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
        this.loadingData.complete();
    }

    loadData(pageNumber : number, size : number) {
        this.loadingData.next(true);

        this.dataService.getPagedData(pageNumber, size).pipe(
            catchError(err => {
                this.totalRecords = 0;
                this.notificationService.notifyError(err);
                return of([]);
            }),
            finalize(() => this.loadingData.next(false))
        )
        .subscribe(data => { 
            this.totalRecords = data.total;
            this.dataSubject.next(data.data)
        });
    }   

    loadPaginatorData(paginator : MatPaginator, searchInput? : ElementRef){

        if(searchInput){
            fromEvent(searchInput.nativeElement,'keyup')
                    .pipe(
                        debounceTime(950),
                        distinctUntilChanged(),
                        tap(() => {
                            paginator.pageIndex = 0;
                            this.loadData(paginator.pageIndex + 1, paginator.pageSize);
                        })
                    )
                    .subscribe();
        }

        paginator.page
        .pipe(
            tap(() => {
                this.loadData(paginator.pageIndex + 1, paginator.pageSize);
            })
        )
        .subscribe();
    }
}
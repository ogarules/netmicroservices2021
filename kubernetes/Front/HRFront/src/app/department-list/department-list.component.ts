import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { NotificationMessageService } from '../datasources/notification-message.service';
import { PageableDatasource } from '../datasources/pageable-datasource';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.scss']
})
export class DepartmentListComponent implements OnInit, AfterViewInit {

  dataSource: PageableDatasource;

  @ViewChild(MatPaginator, { static: false })
  paginator: MatPaginator;

  constructor(private notificationService : NotificationMessageService, private departmentService : DepartmentService) { }

  ngOnInit(): void {
    this.dataSource = new PageableDatasource(this.departmentService, this.notificationService);
    this.dataSource.loadData(0, 10);
  }

  ngAfterViewInit() {
    this.dataSource.loadPaginatorData(this.paginator);
  }
}

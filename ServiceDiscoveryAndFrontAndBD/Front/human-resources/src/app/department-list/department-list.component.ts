import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { PageableDatasource } from '../pageable-datasource';
import { DepartmentService } from '../services/department.service';
import { NotificationMessageService } from '../services/notification-message.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.scss']
})
export class DepartmentListComponent implements OnInit, AfterViewInit {

  dataSource: PageableDatasource;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  constructor(private notificationService: NotificationMessageService, private departmentService : DepartmentService) { }

  ngOnInit(): void {
    this.dataSource = new PageableDatasource(this.departmentService, this.notificationService);
    this.dataSource.loadData(1, 10);
  }

  ngAfterViewInit() {
    this.dataSource.loadPaginatorData(this.paginator);
  }

  delete(item) {
    
  }
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationMessageService } from '../datasources/notification-message.service';
import { PageableDatasource } from '../datasources/pageable-datasource';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  dataSource: PageableDatasource;

  @ViewChild(MatPaginator, { static: false })
  paginator: MatPaginator;

  constructor(private notificationService: NotificationMessageService, private employeeService: EmployeeService,
              private router : Router, private route : ActivatedRoute) { }

  id = 0;
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.employeeService.id = this.id;
    this.dataSource = new PageableDatasource(this.employeeService, this.notificationService);
    this.dataSource.loadData(0, 10);
  }

  ngAfterViewInit() {
    this.dataSource.loadPaginatorData(this.paginator);
  }

}

import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router, ActivatedRoute } from '@angular/router';
import { PageableDatasource } from '../pageable-datasource';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';
import { NotificationMessageService } from '../services/notification-message.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit, AfterViewInit {

  dataSource: PageableDatasource;
  id = 0;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  constructor(private notificationService: NotificationMessageService, private employeeService: EmployeeService, private router: Router, private route: ActivatedRoute) {
    this.id = this.route.snapshot.params['id'];
    this.employeeService.departmentId = this.id;
   }

  ngOnInit(): void {
    this.dataSource = new PageableDatasource(this.employeeService, this.notificationService);
    this.dataSource.loadData(1, 10);
  }

  ngAfterViewInit() {
    this.dataSource.loadPaginatorData(this.paginator);
  }

  delete(item) {
    
  }

}

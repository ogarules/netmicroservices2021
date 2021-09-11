import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';
import { NotificationMessageService } from '../services/notification-message.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {

  item: any = {};
  id = 0;
  departments = [];
  constructor(private employeeService: EmployeeService, private departmentService: DepartmentService, private notificationService: NotificationMessageService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.departmentService.getPagedData(0, 1000).subscribe(data => {
      this.departments = data.data;
    }, error => this.notificationService.notifyError(error));

    if (this.id > 0) {
      this.employeeService.getById(this.id).subscribe(data => {
        this.item = data;
      }, error => this.notificationService.notifyError(error));
    }
  }

  save() {
    if (this.id == 0) {
      this.employeeService.add(this.item).subscribe(data => {
        this.notificationService.showInformation('Empleado agregado');
        this.router.navigate(['department']);
      }, error => this.notificationService.notifyError(error));
    }
    else {
      this.employeeService.update(this.id, this.item).subscribe(data => {
        this.notificationService.showInformation('Empleado actualizado');
        this.router.navigate(['department']);
      }, error => this.notificationService.notifyError(error));
    }
  }
}

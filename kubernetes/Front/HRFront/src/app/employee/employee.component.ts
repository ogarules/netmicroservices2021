import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationMessageService } from '../datasources/notification-message.service';
import { DepartmentService } from '../department.service';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {

  item: any = {};
  id = 0;
  departments:any[] = [];
  constructor(private notificationService: NotificationMessageService, private employeeService: EmployeeService,
              private departmentService:DepartmentService,
              private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['idEmployee'];

    this.departmentService.getPagedData(0, 200).subscribe(data => {
      this.departments = data.data;
    },error => this.notificationService.notifyError(error));

    if (this.id > 0) {
      this.employeeService.getById(this.id).subscribe(data => {
        this.item = data;
      }, error => this.notificationService.notifyError(error));
    }
  }

  save() {
    if (this.id > 0) {
      this.employeeService.update(this.id, this.item).subscribe(data => {
        this.notificationService.showInformation('El empleado se actualizó correcatmente');
        this.router.navigate(['departments']);
      }, error => this.notificationService.notifyError(error));
    }
    else {
      this.employeeService.add(this.item).subscribe(data => {
        this.notificationService.showInformation('El empleado se agregó correcatmente');
        this.router.navigate(['departments']);
      }, error => this.notificationService.notifyError(error));
    }
  }
}

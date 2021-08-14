import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationMessageService } from '../datasources/notification-message.service';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {

  item: any = {};
  id= 0;
  constructor(private notificationService: NotificationMessageService, private departmentService: DepartmentService,
              private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    if (this.id > 0) {
      this.departmentService.getById(this.id).subscribe(data => {
        this.item = data;
      }, error => this.notificationService.notifyError(error));
    }
  }

  save() {
    if (this.id > 0) {
      this.departmentService.update(this.id, this.item).subscribe(data => {
        this.notificationService.showInformation('El departamento se actualizó correcatmente');
        this.router.navigate(['departments']);
      }, error => this.notificationService.notifyError(error));
    }
    else {
      this.departmentService.add(this.item).subscribe(data => {
        this.notificationService.showInformation('El departamento se agregó correcatmente');
        this.router.navigate(['departments']);
      }, error => this.notificationService.notifyError(error));
    }
  }
}

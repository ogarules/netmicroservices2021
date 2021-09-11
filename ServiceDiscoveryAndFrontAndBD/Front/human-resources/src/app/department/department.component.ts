import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { NotificationMessageService } from '../services/notification-message.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {

  item: any = {};
  id = 0;
  constructor(private departmentService: DepartmentService, private notificationService: NotificationMessageService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    if (this.id > 0) {
      this.departmentService.getById(this.id).subscribe(data => {
        this.item = data;
      }, error => this.notificationService.notifyError(error));
    }
  }

  save() {
    if (this.id == 0) {
      this.departmentService.add(this.item).subscribe(data => {
        this.notificationService.showInformation('Departamento agregado');
        this.router.navigate(['department']);
      }, error => this.notificationService.notifyError(error));
    }
    else {
      this.departmentService.update(this.id, this.item).subscribe(data => {
        this.notificationService.showInformation('Departamento actualizado');
        this.router.navigate(['department']);
      }, error => this.notificationService.notifyError(error));
    }
  }
}

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OktaAuthModule, OktaCallbackComponent, OKTA_CONFIG } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';
import { AuthGuard } from './auth-guard';
import { AuthInterceptor } from './auth-interceptor';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentComponent } from './department/department.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeComponent } from './employee/employee.component';
import { LoginComponent } from './login/login.component';

const oktaConfig = {
  issuer: environment.oktaIssuer,
  redirectUri: window.location.origin + '/callback',
  clientId: environment.oktaClientId,
  scopes: ['openid', 'profile']
};

let authData = {
  isAuthenticated:false
};


const routes: Routes = [
  { path: '', redirectTo: 'department', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'department', component: DepartmentListComponent, canActivate: [AuthGuard] },
  { path: 'department/:id', component: DepartmentComponent, canActivate: [AuthGuard] },
  { path: 'department/:id/employee', component: EmployeeListComponent, canActivate: [AuthGuard] },
  { path: 'employee/:id', component: EmployeeComponent, canActivate: [AuthGuard] },
  { path: 'callback', component: OktaCallbackComponent }
];

@NgModule({
  imports: [
    HttpClientModule,
    OktaAuthModule,
    RouterModule.forRoot(routes)],
  providers: [
      { provide: 'authData', useValue: authData },
      { provide: OKTA_CONFIG, useValue: oktaConfig },
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

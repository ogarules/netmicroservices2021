import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private oauthService: OktaAuthService, private router: Router, @Inject('authData') public authentication) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authentication.isAuthenticated) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
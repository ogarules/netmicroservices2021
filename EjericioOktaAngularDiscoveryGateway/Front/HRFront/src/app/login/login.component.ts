import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public oktaAuth: OktaAuthService, private router: Router, @Inject('authData') public authentication) { }

  async ngOnInit() {
    let isAuthenticated = await this.oktaAuth.isAuthenticated();

    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean) => {
        this.authentication.isAuthenticated = isAuthenticated;
        if (isAuthenticated) {
          this.router.navigate(['/departments']);
        }
      }
    );
    
    if (!isAuthenticated) {
        this.oktaAuth.signInWithRedirect({});        
    }

    if (isAuthenticated) {
      this.router.navigate(['/departments']);
    }
  }

  
}

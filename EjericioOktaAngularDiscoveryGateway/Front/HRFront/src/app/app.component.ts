import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Human resources';
  isAuthenticated: boolean = false;
  user: any;

  constructor(public oktaAuth: OktaAuthService, private router: Router, @Inject('authData') public authentication) {
  }

  async ngOnInit() {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    this.authentication.isAuthenticated = this.isAuthenticated;

    this.oktaAuth.$authenticationState.subscribe(
      (isAuthenticated: boolean) => {
        this.isAuthenticated = isAuthenticated;
        this.authentication.isAuthenticated = this.isAuthenticated;
        this.oktaAuth.getUser().then((value) => this.user = value);
      }
    );

    this.user = await this.oktaAuth.getUser();
  }
}

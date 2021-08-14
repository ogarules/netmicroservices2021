import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { OktaAuthService } from "@okta/okta-angular";
import { Observable, from } from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private oktaAuth: OktaAuthService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.handleAccess(request, next));
  }

    private async handleAccess(request: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {
        console.log('agregando token...');
    // Only add an access token to whitelisted origins
    const allowedOrigins = ['http://192.168.1.150', 'http://192.168.0.20', 'https://localhost', 'http://localhost','http://my.humanresources.com','http://humanresources.com','https://my.humanresources.com','https://humanresources.com'];
    if (allowedOrigins.some(url => request.urlWithParams.includes(url))) {
        const accessToken = await this.oktaAuth.getAccessToken();
        console.log('token =>' + accessToken);
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + accessToken
        }
      });
    }
    return next.handle(request).toPromise();
  }
}

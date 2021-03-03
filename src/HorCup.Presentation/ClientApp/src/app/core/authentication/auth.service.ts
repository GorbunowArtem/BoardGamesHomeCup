import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  public authNavStatus$ = this._authNavStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user!: User | null;

  public constructor(private _http: HttpClient) {
    this.manager.getUser().then((user) => {
      this.user = user;
      this._authNavStatusSource.next(this.isAuthenticated());
    });
  }

  public login() {
    return this.manager.signinRedirect();
  }

  public async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  public register(userRegistration: any) {
    return this._http.post('http://localhost:5001/api/account', userRegistration).pipe();
  }

  public isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  public get authorizationHeaderValue(): string {
    return `${this.user?.token_type} ${this.user?.access_token}`;
  }

  public get name(): string | undefined {
    return this.user != null ? this.user.profile.name : '';
  }

  public async signout() {
    await this.manager.signoutRedirect();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'http://localhost:5000',
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: 'id_token token',
    scope: 'openid profile email api.read',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  };
}

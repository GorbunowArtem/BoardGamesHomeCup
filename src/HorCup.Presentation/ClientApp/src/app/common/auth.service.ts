import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  public authNavStatus$ = this._authNavStatusSource.asObservable();

  private _identityServerUrl = 'https://localhost:5001/api';
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
    return this._http.post(`${this._identityServerUrl}/account`, userRegistration).pipe();
  }

  public isLoginUnique(value: any): Observable<any> {
    return this._http.head(`${this._identityServerUrl}/account/name?name=${value}`, {
      observe: 'response'
    });
  }

  public isEmailUnique(value: any): Observable<any> {
    return this._http.head(`${this._identityServerUrl}/account/email?email=${value}`, {
      observe: 'response'
    });
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
    authority: 'https://localhost:5001',
    client_id: 'HorCup.SPA',
    post_logout_redirect_uri: 'https://localhost:5002',
    popup_post_logout_redirect_uri: 'https://localhost:5002',
    redirect_uri: 'https://localhost:5002/auth-callback',
    response_type: 'code',
    scope: 'openid profile api.admin',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: 'https://localhost:5002/silent-refresh.html'
  };
}

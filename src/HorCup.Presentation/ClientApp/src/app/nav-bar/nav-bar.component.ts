import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { RegisterDialogComponent } from '../account/register/register-dialog.component';
import { AuthService } from '../core/authentication/auth.service';
import { ThemeService } from './theme.service';

@Component({
  selector: 'hc-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit, OnDestroy {
  public isDarkTheme!: Observable<boolean>;

  public name!: string | undefined;
  public authenticated!: boolean;
  public subscription!: Subscription;

  public constructor(
    private _themeService: ThemeService,
    private _matDialog: MatDialog,
    private _authService: AuthService
  ) {}

  public ngOnInit() {
    this.isDarkTheme = this._themeService.isDarkTheme;
    this.subscription = this._authService.authNavStatus$.subscribe(
      (status) => (this.authenticated = status)
    );
    this.name = this._authService.name;
  }

  public toggleDarkTheme(checked: boolean) {
    this._themeService.setDarkTheme(checked);
  }

  public showRegisterDialog() {
    this._matDialog.open(RegisterDialogComponent, { disableClose: true });
  }

  public showLoginDialog() {
    this._authService.login();
  }

  public async signout() {
    await this._authService.signout();
  }

  public ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }
}

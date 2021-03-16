import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { RegisterDialogComponent } from '../account/register/register-dialog.component';
import { AuthService } from '../common/auth.service';
import { ThemeService } from './theme.service';

@Component({
  selector: 'hc-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit, OnDestroy {
  public isDarkTheme!: Observable<boolean>;

  public authenticated!: boolean;
  public subscription!: Subscription;

  public constructor(
    private _themeService: ThemeService,
    private _matDialog: MatDialog,
    public authService: AuthService
  ) {}

  public ngOnInit() {
    this.isDarkTheme = this._themeService.isDarkTheme;
    this.subscription = this.authService.authNavStatus$.subscribe(
      (status) => (this.authenticated = status)
    );
  }

  public toggleDarkTheme(checked: boolean) {
    this._themeService.setDarkTheme(checked);
  }

  public showRegisterDialog() {
    this._matDialog.open(RegisterDialogComponent, { disableClose: true });
  }

  public showLoginDialog() {
    this.authService.login();
  }

  public async signout() {
    await this.authService.signout();
  }

  public ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }
}

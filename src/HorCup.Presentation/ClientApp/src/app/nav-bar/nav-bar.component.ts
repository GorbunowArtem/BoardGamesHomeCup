import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { RegisterDialogComponent } from '../account/register/register-dialog.component';
import { ThemeService } from './theme.service';

@Component({
  selector: 'hc-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  public isDarkTheme!: Observable<boolean>;

  public constructor(private _themeService: ThemeService, private _registerDialog: MatDialog) {}

  public ngOnInit() {
    this.isDarkTheme = this._themeService.isDarkTheme;
  }

  public toggleDarkTheme(checked: boolean) {
    this._themeService.setDarkTheme(checked);
  }

  public showRegisterDialog() {
    this._registerDialog.open(RegisterDialogComponent, { disableClose: true });
  }
}

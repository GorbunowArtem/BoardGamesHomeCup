import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ThemeService } from 'src/app/common/theme.service';

@Component({
  selector: 'hc-user-settings-dialog',
  templateUrl: './user-settings-dialog.component.html',
  styleUrls: ['./user-settings-dialog.component.scss']
})
export class UserSettingsDialogComponent implements OnInit {
  public isDarkTheme!: Observable<boolean>;

  public constructor(private _themeService: ThemeService) {}

  public ngOnInit() {
    this.isDarkTheme = this._themeService.isDarkTheme;
  }

  public toggleDarkTheme(checked: boolean) {
    this._themeService.setDarkTheme(checked);
  }
}

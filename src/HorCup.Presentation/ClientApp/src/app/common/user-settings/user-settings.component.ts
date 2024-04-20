import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserSettingsDialogComponent } from './user-settings-dialog/user-settings-dialog.component';

@Component({
  selector: 'hc-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss']
})
export class UserSettingsComponent {
  public constructor(private _settingsDialog: MatDialog) {}

  public openDialog() {
    this._settingsDialog.open(UserSettingsDialogComponent);
  }
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';

import { HcFieldValidationErrorsComponent } from './field-validation-errors/field-validation-errors.component';
import { HcAvatarComponent } from './hc-avatar/hc-avatar.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { HeaderCardComponent } from './header-card/header-card.component';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AddItemComponent } from './add-item/add-item.component';
import { RouterModule } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatRippleModule } from '@angular/material/core';
import { BottomNavComponent } from './bottom-nav/bottom-nav';
import { SidenavComponent } from './sidenav/sidenav.component';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatToolbarModule,
    RouterModule,
    MatSlideToggleModule,
    MatRippleModule,
    MatSidenavModule
  ],
  declarations: [
    HcFieldValidationErrorsComponent,
    HcAvatarComponent,
    ConfirmationDialogComponent,
    HeaderCardComponent,
    AddItemComponent,
    BottomNavComponent,
    SidenavComponent
  ],
  exports: [
    HcFieldValidationErrorsComponent,
    HcAvatarComponent,
    ConfirmationDialogComponent,
    HeaderCardComponent,
    AddItemComponent,
    BottomNavComponent,
    SidenavComponent
  ]
})
export class HcCommonModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

import { NavBarComponent } from './nav-bar.component';
import { BottomNavComponent } from './bottom-nav/bottom-nav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AccountModule } from '../account/account.module';
import { MatRippleModule } from '@angular/material/core';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    RouterModule,
    MatIconModule,
    MatSlideToggleModule,
    AccountModule,
    MatRippleModule
  ],
  declarations: [NavBarComponent, BottomNavComponent],
  exports: [NavBarComponent, BottomNavComponent]
})
export class NavBarModule {}

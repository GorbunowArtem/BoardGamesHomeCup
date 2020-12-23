import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

import { NavBarComponent } from './nav-bar.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  imports: [CommonModule, MatToolbarModule, MatButtonModule, RouterModule, MatIconModule],
  declarations: [NavBarComponent, FooterComponent],
  exports: [NavBarComponent, FooterComponent]
})
export class NavBarModule {}

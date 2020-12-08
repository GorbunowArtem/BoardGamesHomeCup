import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaysComponent } from './plays.component';
import { PlaysRoutingModule } from './plays-routing-module';

@NgModule({
  imports: [CommonModule, PlaysRoutingModule],
  declarations: [PlaysComponent]
})
export class PlaysModule {}

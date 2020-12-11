import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaysComponent } from './plays.component';
import { PlaysRoutingModule } from './plays-routing-module';
import { MatExpansionModule } from '@angular/material/expansion';
import { PlayPanelComponent } from './play-panel/play-panel.component';
@NgModule({
  imports: [CommonModule, PlaysRoutingModule, MatExpansionModule],
  declarations: [PlaysComponent, PlayPanelComponent]
})
export class PlaysModule {}

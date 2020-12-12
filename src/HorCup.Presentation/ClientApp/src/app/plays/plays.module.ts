import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaysComponent } from './plays.component';
import { PlaysRoutingModule } from './plays-routing-module';
import { MatExpansionModule } from '@angular/material/expansion';
import { PlayPanelComponent } from './play-panel/play-panel.component';
import { MatTableModule } from '@angular/material/table';
import { HcCommonModule } from '../common/hc-common.module';
@NgModule({
  imports: [CommonModule, PlaysRoutingModule, MatExpansionModule, MatTableModule, HcCommonModule],
  declarations: [PlaysComponent, PlayPanelComponent]
})
export class PlaysModule {}

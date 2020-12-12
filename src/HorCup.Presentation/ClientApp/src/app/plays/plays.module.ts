import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaysComponent } from './plays.component';
import { PlaysRoutingModule } from './plays-routing-module';
import { MatExpansionModule } from '@angular/material/expansion';
import { PlayPanelComponent } from './play-panel/play-panel.component';
import { MatTableModule } from '@angular/material/table';
import { HcCommonModule } from '../common/hc-common.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatStepperModule } from '@angular/material/stepper';
import { AddPlayComponent } from './add-play/add-play.component';
import { ReactiveFormsModule } from '@angular/forms';
@NgModule({
  imports: [
    CommonModule,
    PlaysRoutingModule,
    MatExpansionModule,
    MatTableModule,
    HcCommonModule,
    MatButtonModule,
    MatIconModule,
    MatStepperModule,
    ReactiveFormsModule
  ],
  declarations: [PlaysComponent, PlayPanelComponent, AddPlayComponent]
})
export class PlaysModule {}

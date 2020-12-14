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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';

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
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule
  ],
  declarations: [PlaysComponent, PlayPanelComponent, AddPlayComponent]
})
export class PlaysModule {}

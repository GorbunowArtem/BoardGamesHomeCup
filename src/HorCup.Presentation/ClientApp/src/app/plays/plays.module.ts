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
import { MatNativeDateModule, MatRippleModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PlaysFilterComponent } from './plays-filter/plays-filter.component';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { PlaysNavBarComponent } from './plays-nav-bar/plays-nav-bar.component';

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
    MatAutocompleteModule,
    MatNativeDateModule,
    MatRippleModule,
    MatDatepickerModule,
    MatDividerModule,
    MatSnackBarModule,
    MatSelectModule,
    MatChipsModule,
    MatCardModule,
    MatToolbarModule
  ],
  declarations: [
    PlaysComponent,
    PlayPanelComponent,
    AddPlayComponent,
    PlaysFilterComponent,
    PlaysNavBarComponent
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }]
})
export class PlaysModule {}

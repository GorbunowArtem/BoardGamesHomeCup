import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PlayersComponent } from './players.component';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { PlayersRoutingModule } from './players-routing-module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatNativeDateModule, MatRippleModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { HcCommonModule } from '../common/hc-common.module';
import { HttpClientModule } from '@angular/common/http';
import { UniqueNicknameValidator } from './add-edit-player-dialog/unique-nickname-validator';
import { PlayerCardComponent } from './player-card/player-card.component';
import { PlayerDetailsComponent } from './player-details/player-details.component';

@NgModule({
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    PlayersRoutingModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatRippleModule,
    HcCommonModule,
    HttpClientModule,
    MatPaginatorModule,
    MatCardModule,
    MatMenuModule
  ],
  declarations: [
    PlayersComponent,
    AddEditPlayerDialogComponent,
    UniqueNicknameValidator,
    PlayerCardComponent,
    PlayerDetailsComponent
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }]
})
export class PlayersModule {}

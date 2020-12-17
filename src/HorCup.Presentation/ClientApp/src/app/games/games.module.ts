import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GamesComponent } from './games.component';
import { GamesRoutingModule } from './games-routing-module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { GameCardComponent } from './game-card/game-card.component';
import { MatCardModule } from '@angular/material/card';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { BarRatingModule } from 'ngx-bar-rating';
import { GamesFilterComponent } from './games-filter/games-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AddEditGameDialogComponent } from './add-edit-game-dialog/add-edit-game-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { HcCommonModule } from '../common/hc-common.module';
import { GameDetailsComponent } from './game-details/game-details.component';

@NgModule({
  imports: [
    CommonModule,
    GamesRoutingModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatInputModule,
    MatPaginatorModule,
    MatCardModule,
    BarRatingModule,
    MatBottomSheetModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatToolbarModule,
    MatDialogModule,
    HcCommonModule
  ],
  declarations: [
    GamesComponent,
    GameCardComponent,
    GamesFilterComponent,
    AddEditGameDialogComponent,
    GameDetailsComponent
  ]
})
export class GamesModule {}

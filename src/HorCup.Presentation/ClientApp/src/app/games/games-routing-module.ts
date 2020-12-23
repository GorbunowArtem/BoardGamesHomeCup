import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameDetailsComponent } from './game-details/game-details.component';
import { GamesComponent } from './games.component';

const routes: Routes = [
  { path: '', component: GamesComponent },
  { path: 'disciplines/:id', component: GameDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GamesRoutingModule {}

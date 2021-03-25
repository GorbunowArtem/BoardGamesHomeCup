import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './account/auth-callback/auth-callback.component';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./plays/plays.module').then((m) => m.PlaysModule)
  },
  {
    path: 'participants',
    loadChildren: () => import('./players/players.module').then((m) => m.PlayersModule)
  },
  {
    path: 'disciplines',
    loadChildren: () => import('./games/games.module').then((m) => m.GamesModule)
  },
  { path: 'auth-callback', component: AuthCallbackComponent },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPlayComponent } from './add-play/add-play.component';
import { PlaysComponent } from './plays.component';

const routes: Routes = [
  {
    path: '',
    component: PlaysComponent
  },
  {
    path: 'add-play',
    component: AddPlayComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlaysRoutingModule {}

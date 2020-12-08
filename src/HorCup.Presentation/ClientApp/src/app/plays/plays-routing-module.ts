import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlaysComponent } from './plays.component';

const routes: Routes = [
  {
    path: '',
    component: PlaysComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlaysRoutingModule {}

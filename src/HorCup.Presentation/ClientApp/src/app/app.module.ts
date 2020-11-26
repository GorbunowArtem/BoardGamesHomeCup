import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PlayersModule } from './players/players.module';
import { NavBarModule } from './nav-bar/nav-bar.module';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, PlayersModule, NavBarModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}

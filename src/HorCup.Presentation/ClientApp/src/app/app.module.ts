import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';

import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PlayersModule } from './players/players.module';
import { NavBarModule } from './nav-bar/nav-bar.module';
import { GamesModule } from './games/games.module';
import { PlaysModule } from './plays/plays.module';
import { FlexLayoutModule } from '@angular/flex-layout';

registerLocaleData(localeRu, 'ru');

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PlayersModule,
    NavBarModule,
    GamesModule,
    PlaysModule,
    FlexLayoutModule
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'ru' }],
  bootstrap: [AppComponent]
})
export class AppModule {}

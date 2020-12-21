import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

@Component({
  selector: 'hc-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit, OnDestroy {
  public players: Player[] = [];

  public totalItems = 0;

  public playerAddedSubscription!: Subscription;

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {}

  public showDialog() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }
  public ngOnInit() {
    this.search();
    this.playerAddedSubscription = this._playersService
      .countChanged()
      .subscribe(() => this.search());
  }

  public ngOnDestroy() {
    this.playerAddedSubscription.unsubscribe();
  }

  public pageChangedEvent(event: PageEvent) {
    this.search(event.pageSize, event.pageSize * event.pageIndex);
  }

  public search(take: number = 6, skip: number = 0, searchText = '') {
    this._playersService
      .search(new SearchPlayersOptions(take, skip, searchText))
      .subscribe((result) => {
        this.players = result.items;
        this.totalItems = result.total;
      });
  }
}

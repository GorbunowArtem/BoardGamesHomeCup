import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

@Component({
  selector: 'hc-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit {
  @Input()
  searchText: string = '';

  players: Player[] = [];

  totalItems: number = 0;

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {}

  public showDialog() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }
  ngOnInit() {
    this._playersService.search(new SearchPlayersOptions()).subscribe((result) => {
      this.players = result.items;
      this.totalItems = result.total;
    });
  }

  pageChangedEvent(event: PageEvent) {
    this._playersService
      .search(
        new SearchPlayersOptions(event.pageSize * event.pageIndex, event.pageSize, this.searchText)
      )
      .subscribe((result) => {
        this.players = result.items;
        this.totalItems = result.total;
      });
  }

  public search() {
    this._playersService
      .search(new SearchPlayersOptions(5, 0, this.searchText))
      .subscribe((result) => {
        this.players = result.items;
        this.totalItems = result.total;
      });
  }
}

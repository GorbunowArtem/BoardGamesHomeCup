import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { IState } from '../common/models/i-state';
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
  private readonly _playersPerPage = 10;

  public players: Player[];

  public totalItems = 0;

  public playersCountChangedSubscription!: Subscription;

  private searchOptions;

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {
    this.players = [];
    this.searchOptions = new SearchPlayersOptions();
  }

  public addPlayer() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }

  public ngOnInit() {
    this.search();
    this.playersCountChangedSubscription = this._playersService
      .stateChanged()
      .subscribe((result) => {
        this.onPlayerStateChanged(result);
      });
  }

  public ngOnDestroy() {
    this.playersCountChangedSubscription.unsubscribe();
  }

  public search() {
    this._playersService.search(this.searchOptions).subscribe((result) => {
      this.players.push(...result.items.$values);
      this.totalItems = result.total;
    });
  }

  @HostListener('window:scroll', ['$event'])
  public onScroll() {
    if (window.innerHeight + window.scrollY === document.body.scrollHeight) {
      this.loadMore();
    }
  }

  private loadMore() {
    if (this.totalItems > this.players.length) {
      this.searchOptions.take += this.searchOptions.take;
      this.searchOptions.skip = this.searchOptions.take - this._playersPerPage;

      this.search();
    }
  }

  private onPlayerStateChanged(state: IState<Player>) {
    if (state.added) {
      this.players.unshift(state.added);
    } else if (state.edited) {
      const index = this.players.findIndex((item) => item.id === state.edited.id);
      if (index > -1) {
        this.players[index] = state.edited;
      }
    } else if (state.removed) {
      const index = this.players.findIndex((item) => item.id === state.removed.id);
      if (index > -1) {
        this.players.splice(index, 1);
      }
    }
  }
}

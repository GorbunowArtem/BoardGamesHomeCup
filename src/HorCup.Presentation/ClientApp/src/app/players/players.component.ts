import { Component, HostListener, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
  public players;

  public totalItems = 0;

  public playersCountChangedSubscription!: Subscription;

  private searchOptions;

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {
    this.players = new Set<Player>();
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
      .countChanged()
      .subscribe((result) => {
        // TODO: Add delete/edit/add logic for new Players
        console.log(result);
      });
  }

  public ngOnDestroy() {
    this.playersCountChangedSubscription.unsubscribe();
  }

  public search() {
    this._playersService.search(this.searchOptions).subscribe((result) => {
      result.items.$values.forEach((player) => this.players.add(player));
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
    if (this.totalItems > this.players.size) {
      this.searchOptions.take += this.searchOptions.take;
      this.searchOptions.skip = this.searchOptions.take - 10;

      this.search();
    }
  }
}

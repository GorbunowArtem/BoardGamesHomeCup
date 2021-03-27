import { Component, HostListener, Input, OnDestroy, OnInit } from '@angular/core';
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

  private searchOptions = new SearchPlayersOptions();

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {}

  public addPlayer() {
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
      this.searchOptions.skip = this.searchOptions.take - 10;

      this.search();
    }
  }
}

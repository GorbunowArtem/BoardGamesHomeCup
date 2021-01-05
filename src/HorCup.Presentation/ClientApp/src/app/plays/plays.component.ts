import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';
import { PlaysFilterComponent } from './plays-filter/plays-filter.component';
import { PlaysService } from './plays.service';

@Component({
  selector: 'hc-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.scss']
})
export class PlaysComponent implements OnInit {
  private _plays: Play[];

  private take = 10;
  private total = 0;

  public constructor(private _playsService: PlaysService, private _bottomSheet: MatBottomSheet) {
    this._plays = [];
  }

  public ngOnInit() {
    this._playsService.search(new SearchPlaysOptions(0, this.take)).subscribe((plays) => {
      this._plays = plays.items;
      this.total = plays.total;
    });
  }

  public get plays() {
    return this._plays;
  }

  public get moreAvailable() {
    return this.total > this.take;
  }

  public loadMore() {
    this.take += this.take;
    this._playsService
      .search(new SearchPlaysOptions(this.take - 10, this.take))
      .subscribe((plays) => {
        this._plays.push(...plays.items);
      });
  }

  public openFilter() {
    this._bottomSheet.open(PlaysFilterComponent);
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Subscription } from 'rxjs';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';
import { PlaysFilterComponent } from './plays-filter/plays-filter.component';
import { PlaysService } from './plays.service';

@Component({
  selector: 'hc-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.scss']
})
export class PlaysComponent implements OnInit, OnDestroy {
  private _plays: Play[];

  private take = 10;
  private total = 0;

  private _searchOptions: SearchPlaysOptions;

  private _searchParamsChangedSubscription!: Subscription;

  public constructor(private _playsService: PlaysService, private _playsFilter: MatBottomSheet) {
    this._plays = [];
    this._searchOptions = new SearchPlaysOptions(0, 10);
  }

  public ngOnInit() {
    this.search();
    this._searchParamsChangedSubscription = this._playsService.searchParamsChangedSubject.subscribe(
      (options) => {
        this._searchOptions = options;
        this.search();
      }
    );
  }

  public ngOnDestroy(): void {
    this._searchParamsChangedSubscription.unsubscribe();
  }

  public search() {
    this._playsService.search(this._searchOptions).subscribe((result) => {
      this._plays = result.items;
      this.total = result.total;
      window.scrollTo(0, 0);
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
    this._playsFilter.open(PlaysFilterComponent, {
      data: this._searchOptions
    });
  }
}

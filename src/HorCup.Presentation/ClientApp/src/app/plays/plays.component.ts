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
  public total = 0;

  private _plays: Play[];

  private _take = 10;

  private _searchOptions: SearchPlaysOptions;

  private _searchParamsChangedSubscription!: Subscription;

  public viewMode: boolean;

  public searchText: string;

  public constructor(private _playsService: PlaysService, private _playsFilter: MatBottomSheet) {
    this._plays = [];
    this.viewMode = true;
    this._searchOptions = new SearchPlaysOptions(0, 10);
    this.searchText = '';
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
      this._plays = result.items.$values;
      this.total = result.total;
      window.scrollTo(0, 0);
    });
  }
  public get plays() {
    return this._plays;
  }

  public get moreAvailable() {
    return this.total > this._take;
  }

  public loadMore() {
    this._take += this._take;
    this._playsService
      .search(new SearchPlaysOptions(this._take - 10, this._take))
      .subscribe((plays) => {
        this._plays.push(...plays.items.$values);
      });
  }

  public openFilter() {
    this._playsFilter.open(PlaysFilterComponent, {
      data: this._searchOptions
    });
  }

  public toggleViewMode() {
    this.searchText = '';
    this.viewMode = !this.viewMode;
  }
}

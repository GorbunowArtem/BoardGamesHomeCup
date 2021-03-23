import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
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

  public viewMode: boolean;

  private _plays: Play[];

  public searchOptions: SearchPlaysOptions;

  private _searchParamsChangedSubscription!: Subscription;

  private _searchTextChanged: Subject<string> = new Subject<string>();

  public constructor(private _playsService: PlaysService, private _playsFilter: MatBottomSheet) {
    this._plays = [];
    this.viewMode = true;
    this.searchOptions = new SearchPlaysOptions();
    this._searchTextChanged.pipe(debounceTime(500), distinctUntilChanged()).subscribe((model) => {
      this.searchOptions.searchText = model;
      this.search();
    });
  }

  public ngOnInit() {
    this.search();
    this._searchParamsChangedSubscription = this._playsService.searchParamsChangedSubject.subscribe(
      (options) => {
        this.searchOptions = options;
        this.search();
      }
    );
  }

  public ngOnDestroy(): void {
    this._searchParamsChangedSubscription.unsubscribe();
  }

  public search() {
    this._playsService.search(this.searchOptions).subscribe((result) => {
      this._plays = result.items.$values;
      this.total = result.total;
      window.scrollTo(0, 0);
    });
  }
  public get plays() {
    return this._plays;
  }

  public get moreAvailable() {
    return this.total > this.searchOptions.take;
  }

  public loadMore() {
    this.searchOptions.take += this.searchOptions.take;
    this.searchOptions.skip = this.searchOptions.take - 10;

    this._playsService.search(this.searchOptions).subscribe((plays) => {
      this._plays.push(...plays.items.$values);
    });
  }

  public openFilter() {
    this._playsFilter.open(PlaysFilterComponent, {
      data: this.searchOptions
    });
  }

  public toggleViewMode() {
    if (this.searchOptions.searchText !== '') {
      this._searchTextChanged.next('');
    }

    this.viewMode = !this.viewMode;
  }

  public textChanged(text: string) {
    this._searchTextChanged.next(text);
  }
}

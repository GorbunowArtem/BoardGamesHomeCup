import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';
import { PlaysService } from './plays.service';
@Component({
  selector: 'hc-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.scss']
})
export class PlaysComponent implements OnInit, OnDestroy {
  public total = 0;

  private _plays: Play[];

  public searchOptions: SearchPlaysOptions;

  private _searchParamsChangedSubscription!: Subscription;

  private _searchTextChanged: Subject<string> = new Subject<string>();

  public constructor(private _playsService: PlaysService) {
    this._plays = [];
    this.searchOptions = new SearchPlaysOptions();
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
}

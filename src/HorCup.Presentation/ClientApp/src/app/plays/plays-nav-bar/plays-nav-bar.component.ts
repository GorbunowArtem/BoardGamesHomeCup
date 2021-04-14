import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { SidenavService } from 'src/app/common/sidenav/sidenav.service';
import { SearchPlaysOptions } from '../models/search-plays-options';
import { PlaysFilterComponent } from '../plays-filter/plays-filter.component';
import { PlaysService } from '../plays.service';

@Component({
  selector: 'hc-plays-nav-bar',
  templateUrl: './plays-nav-bar.component.html',
  styleUrls: ['./plays-nav-bar.component.scss']
})
export class PlaysNavBarComponent {
  public viewMode: boolean;

  public toggleActive: boolean;

  private _searchTextChanged: Subject<string> = new Subject<string>();

  public searchOptions: SearchPlaysOptions;

  public constructor(
    private _playsService: PlaysService,
    private _playsFilter: MatBottomSheet,
    private _sideNavService: SidenavService
  ) {
    this.viewMode = true;
    this.toggleActive = false;
    this.searchOptions = new SearchPlaysOptions();
    this._searchTextChanged.pipe(debounceTime(500), distinctUntilChanged()).subscribe((model) => {
      this.searchOptions.searchText = model;
      this._playsService.searchParamsChangedSubject.next(this.searchOptions);
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

  public toggleSidenav() {
    this.toggleActive = !this.toggleActive;
    this._sideNavService.toggle();
  }
}

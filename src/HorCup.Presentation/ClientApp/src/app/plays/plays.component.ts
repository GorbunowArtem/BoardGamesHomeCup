import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { PageableDataSource } from '../common/pageable-data-source/pageable-data-source';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';
import { PlaysService } from './plays.service';
@Component({
  selector: 'hc-plays',
  templateUrl: './plays.component.html',
  styleUrls: ['./plays.component.scss']
})
export class PlaysComponent {
  public plays: PageableDataSource;

  public constructor(private _playsService: PlaysService) {
    this.plays = new PageableDataSource(_playsService, new SearchPlaysOptions());
  }
}

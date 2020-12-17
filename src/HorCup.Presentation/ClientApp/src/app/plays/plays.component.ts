import { Component, OnInit } from '@angular/core';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';
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

  constructor(private playsService: PlaysService) {
    this._plays = [];
  }

  ngOnInit() {
    this.playsService.get(new SearchPlaysOptions(0, this.take)).subscribe((plays) => {
      this._plays = plays.items;
      this.total = plays.total;
    });
  }

  get plays() {
    return this._plays;
  }

  get moreAvailable() {
    return this.total > this.take;
  }

  public loadMore() {
    this.take += this.take;
    this.playsService.get(new SearchPlaysOptions(this.take - 10, this.take)).subscribe((plays) => {
      this._plays.push(...plays.items);
    });
  }
}

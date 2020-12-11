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
  public plays: Play[];

  constructor(private playsService: PlaysService) {
    this.plays = [];
  }

  ngOnInit() {
    this.playsService.get(new SearchPlaysOptions()).subscribe((plays) => {
      this.plays = plays.items;
    });
  }
}

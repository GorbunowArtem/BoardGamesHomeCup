import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'hc-plays-filter',
  templateUrl: './plays-filter.component.html',
  styleUrls: ['./plays-filter.component.scss']
})
export class PlaysFilterComponent implements OnInit {
  public playsFilter: FormGroup;

  public constructor(private _fb: FormBuilder) {
    this.playsFilter = this._fb.group({
      playersIds: [''],
      gamesIds: [''],
      dateFrom: [''],
      dateTo: ['']
    });
  }

  public fruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry'];

  public remove(fruit: string): void {
    const index = this.fruits.indexOf(fruit);

    if (index >= 0) {
      this.fruits.splice(index, 1);
    }
  }
  public ngOnInit() {}
  public reset() {}
  public search() {}
}

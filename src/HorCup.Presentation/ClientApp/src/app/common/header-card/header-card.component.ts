import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'hc-header-card',
  templateUrl: './header-card.component.html',
  styleUrls: ['./header-card.component.scss']
})
export class HeaderCardComponent {
  @Input()
  text: string = '';

  @Input()
  hideNav: boolean = false;

  public constructor(private location: Location) {}

  public goBack() {
    this.location.back();
  }
}

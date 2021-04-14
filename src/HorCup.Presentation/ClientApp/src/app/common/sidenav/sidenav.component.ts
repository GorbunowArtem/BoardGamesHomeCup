import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { SidenavService } from './sidenav.service';

@Component({
  selector: 'hc-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  @ViewChild('sidenav', { static: true })
  public sidenav!: MatSidenav;

  @Input()
  public openNav!: boolean;

  public constructor(private _sidenavService: SidenavService) {}

  public ngOnInit(): void {
    this._sidenavService.setSidenav(this.sidenav);
  }

  public closeSidenav() {
    this._sidenavService.close();
  }
}

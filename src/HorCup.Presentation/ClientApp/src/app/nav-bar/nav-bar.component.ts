import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ThemeService } from './theme.service';

@Component({
  selector: 'hc-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  public isDarkTheme!: Observable<boolean>;

  public constructor(private themeService: ThemeService) {}

  public ngOnInit() {
    this.isDarkTheme = this.themeService.isDarkTheme;
  }

  public toggleDarkTheme(checked: boolean) {
    this.themeService.setDarkTheme(checked);
  }
}

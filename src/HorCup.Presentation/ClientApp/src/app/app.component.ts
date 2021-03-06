import { OverlayContainer } from '@angular/cdk/overlay';
import { Component, HostBinding, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { slideInAnimation } from './animations';
import { ThemeService } from './common/theme.service';

@Component({
  selector: 'hc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [slideInAnimation]
})
export class AppComponent implements OnInit {
  public title = 'hor-cup';
  public isDarkTheme!: Observable<boolean>;

  @HostBinding('class')
  public componentCssClass!: any;

  public constructor(
    public overlayContainer: OverlayContainer,
    private themeService: ThemeService
  ) {}
  public ngOnInit(): void {
    this.isDarkTheme = this.themeService.isDarkTheme;
  }

  public prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}

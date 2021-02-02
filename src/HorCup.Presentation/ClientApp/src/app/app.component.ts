import { OverlayContainer } from '@angular/cdk/overlay';
import { Component, HostBinding } from '@angular/core';
import { CommonService } from './common/common.service';

@Component({
  selector: 'hc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title = 'hor-cup';

  @HostBinding('class')
  public componentCssClass!: any;

  public constructor(
    private _commonService: CommonService,
    public overlayContainer: OverlayContainer
  ) {
    this._commonService.init();
  }

  public onSetTheme(theme: any) {
    this.overlayContainer.getContainerElement().classList.add(theme);
    this.componentCssClass = theme;
  }
}

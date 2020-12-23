import { Component } from '@angular/core';
import { CommonService } from './common/common.service';

@Component({
  selector: 'hc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title = 'hor-cup';

  public constructor(private _commonService: CommonService) {
    this._commonService.init();
  }
}

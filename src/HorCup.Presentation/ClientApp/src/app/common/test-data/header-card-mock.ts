import { Component, Input } from '@angular/core';

@Component({
  selector: 'hc-header-card',
  template: `<div>Header</div>`
})
export class HeaderCardMockComponent {
  @Input()
  public text = '';

  @Input()
  public hideNav = false;
}

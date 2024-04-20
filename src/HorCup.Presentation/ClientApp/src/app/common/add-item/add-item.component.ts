import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'hc-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent {
  @Output() public showAddDialog = new EventEmitter();
}

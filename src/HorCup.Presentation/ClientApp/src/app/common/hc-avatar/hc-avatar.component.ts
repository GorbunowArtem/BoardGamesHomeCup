import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'hc-avatar',
  templateUrl: './hc-avatar.component.html',
  styleUrls: ['./hc-avatar.component.scss']
})
export class HcAvatarComponent implements OnInit {
  @Input()
  public name!: string;

  public initials!: string;
  public circleColor: string = '#EB7181';

  private colors = ['#aea1ff', '#468547', '#FFD558', '#3670B2', '#49f5e4', '#6956e8', '#f5c698'];

  ngOnInit() {
    this.createInititals();

    const randomIndex = Math.floor(Math.random() * Math.floor(this.colors.length));
    this.circleColor = this.colors[randomIndex];
  }

  private createInititals(): void {
    let initials = '';

    for (let i = 0; i < this.name.length; i++) {
      if (this.name.charAt(i) === ' ') {
        continue;
      }

      if (this.name.charAt(i) === this.name.charAt(i).toUpperCase()) {
        initials += this.name.charAt(i);

        if (initials.length == 2) {
          break;
        }
      }
    }

    this.initials = initials;
  }
}

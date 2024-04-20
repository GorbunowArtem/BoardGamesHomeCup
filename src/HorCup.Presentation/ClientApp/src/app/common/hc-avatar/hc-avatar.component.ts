import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'hc-avatar',
  templateUrl: './hc-avatar.component.html',
  styleUrls: ['./hc-avatar.component.scss']
})
export class HcAvatarComponent implements OnInit {
  @Input()
  public name!: string;

  public circleColor = '#EB7181';

  private colors = ['#aea1ff', '#468547', '#FFD558', '#3670B2', '#49f5e4', '#6956e8', '#f5c698'];

  public ngOnInit() {
    const randomIndex = Math.floor(Math.random() * Math.floor(this.colors.length));
    this.circleColor = this.colors[randomIndex];
  }

  public get initials(): string {
    if (this.name) {
      const [firstName, lastName] = this.name.split(' ');

      if (lastName) {
        return `${firstName.charAt(0).toUpperCase()}${lastName.charAt(0).toUpperCase()}`;
      }

      return firstName.substring(0, 2).toUpperCase();
    }

    return '';
  }
}

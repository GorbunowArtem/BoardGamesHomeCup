import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/authentication/auth.service';

@Component({
  selector: 'hc-app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public title = 'Login';
  public constructor(private authService: AuthService) {}

  public login() {
    this.authService.login();
  }
}

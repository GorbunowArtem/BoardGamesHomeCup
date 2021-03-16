import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/common/auth.service';

@Component({
  selector: 'hc-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {
  public error: boolean;

  public constructor(
    private _authService: AuthService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {
    this.error = false;
  }

  public ngOnInit() {
    if (this._route.snapshot?.fragment?.indexOf('error') >= 0) {
      this.error = true;
      return;
    }
    this._authService.completeAuthentication().then(() => {
      this._router.navigate(['/']);
    });
  }
}

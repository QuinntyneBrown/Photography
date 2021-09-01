import { Component } from '@angular/core';
import { AuthService, NavigationService } from '@core';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  private readonly _destroyed$: Subject<void> = new Subject();

  public handleLogin(credentials:any) {
    this._authService.tryToLogin(credentials)
    .pipe(
      takeUntil(this._destroyed$),
      tap(_ => this._navigationService.redirectPreLogin())
    ).subscribe();
  }

  constructor(
    private readonly _authService: AuthService,
    private readonly _navigationService: NavigationService
  ) {

  }
}

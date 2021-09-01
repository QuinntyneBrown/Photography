import { Component, OnInit } from '@angular/core';
import { AuthService } from '@core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent  {

  public currentUser$ = this._authService.currentUser$;

  constructor(
    private readonly _authService: AuthService
  ) {

  }

  public tryToLogout() {
    this._authService.logout();
  }
}

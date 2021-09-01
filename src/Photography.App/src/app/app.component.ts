import { Component } from '@angular/core';
import { AuthService } from '@core';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public vm$ = this._authService.tryToInitializeCurrentUser()
  .pipe(
    map(_ => true)
  );

  constructor(
    private readonly _authService: AuthService
  ) {

  }
}

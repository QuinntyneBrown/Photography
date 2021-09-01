import { Component } from '@angular/core';
import { PhotoService } from '@api';
import { vw } from '@core/vw';
import { fromEvent, interval } from 'rxjs';
import { debounce, map, shareReplay, startWith, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.scss']
})
export class PhotosComponent {

  _photos$ = this._photoService.get()
  .pipe(
    shareReplay(1)
  );

  public vm$ = fromEvent(window,"resize")
  .pipe(
    debounce(_ => interval(300)),
    startWith(true),
    switchMap(_ => this._photos$),
    map(photos => ({
      photos,
      margin: 10,
      columns: 3,
      columnWidth: vw(25)
    }))
  );

  constructor(
    private readonly _photoService: PhotoService
  ) { }
}

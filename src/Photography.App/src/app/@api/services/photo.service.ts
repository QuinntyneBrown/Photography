import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Photo } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { baseUrl, EntityPage, IPagableService } from '@core';

@Injectable({
  providedIn: 'root'
})
export class PhotoService implements IPagableService<Photo> {

  uniqueIdentifierName: string = "photoId";

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  getPage(options: { pageIndex: number; pageSize: number; }): Observable<EntityPage<Photo>> {
    return this._client.get<EntityPage<Photo>>(`${this._baseUrl}api/photo/page/${options.pageSize}/${options.pageIndex}`)
  }

  public get(): Observable<Photo[]> {
    return this._client.get<{ photos: Photo[] }>(`${this._baseUrl}api/photo`)
      .pipe(
        map(x => x.photos)
      );
  }

  public getById(options: { photoId: string }): Observable<Photo> {
    return this._client.get<{ photo: Photo }>(`${this._baseUrl}api/photo/${options.photoId}`)
      .pipe(
        map(x => x.photo)
      );
  }

  public remove(options: { photo: Photo }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/photo/${options.photo.photoId}`);
  }

  public create(options: { photo: Photo }): Observable<{ photo: Photo }> {
    return this._client.post<{ photo: Photo }>(`${this._baseUrl}api/photo`, { photo: options.photo });
  }
  
  public update(options: { photo: Photo }): Observable<{ photo: Photo }> {
    return this._client.put<{ photo: Photo }>(`${this._baseUrl}api/photo`, { photo: options.photo });
  }
}

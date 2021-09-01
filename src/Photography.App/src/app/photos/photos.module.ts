import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotosRoutingModule } from './photos-routing.module';
import { PhotosComponent } from './photos.component';
import { PhotoGridComponent } from './photo-grid/photo-grid.component';
import { PhotoModule } from '@shared';


@NgModule({
  declarations: [
    PhotosComponent,
    PhotoGridComponent
  ],
  imports: [
    CommonModule,
    PhotoModule,
    PhotosRoutingModule
  ]
})
export class PhotosModule { }

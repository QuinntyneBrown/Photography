import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotoComponent } from './photo.component';



@NgModule({
  declarations: [
    PhotoComponent
  ],
  exports: [
    PhotoComponent
  ],
  imports: [
    CommonModule
  ]
})
export class PhotoModule { }

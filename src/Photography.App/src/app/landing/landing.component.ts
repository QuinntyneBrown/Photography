import { Component, OnInit } from '@angular/core';
import { vh } from '@core/vh';
import { vw } from '@core/vw';

const ratio = 922.22 / 1498.19;

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent {

  get imageWidth() {
    const width = vw(90);
    const height = vh(70);
    const calculatedWidth = width * ratio;
    if (calculatedWidth > height) {
      return height / ratio;
    }
    return width;
  }

  get imageHeight() {
    return this.imageWidth * ratio;
  }


}

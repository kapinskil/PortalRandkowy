import { Component, Input, OnInit } from '@angular/core';
import { Photo } from '../../_models/Photo';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.scss']
})
export class PhotosComponent implements OnInit {

  @Input() photos: Photo[];

  constructor() { }

  ngOnInit() {
  }

}

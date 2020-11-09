import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-music-grid-list',
  templateUrl: './music-grid-list.component.html',
  styleUrls: ['./music-grid-list.component.scss'],
})
export class MusicGridListComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  albumDetail() {
    console.log('entrou');
  }
}

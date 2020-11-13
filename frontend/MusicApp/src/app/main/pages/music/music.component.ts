import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-music',
  templateUrl: './music.component.html',
  styleUrls: ['./music.component.scss']
})
export class MusicComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  detail() {
    this.router.navigate([
        "music",
        "1"
    ]);
  }

}

import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Album from "app/model/album.model";
import { MusicService } from "app/services/music.service";
import { Observable } from "rxjs";

@Component({
    selector: "app-music",
    templateUrl: "./music.component.html",
    styleUrls: ["./music.component.scss"],
})
export class MusicComponent implements OnInit {
    albums: Album[];

    constructor(private router: Router, private service: MusicService) {}

    ngOnInit() {
        this.service.getAlbuns().subscribe((data) => (this.albums = data));
    }

    detail(album: Album) {
        this.router.navigate(["page", "music", album.id]);
    }
}

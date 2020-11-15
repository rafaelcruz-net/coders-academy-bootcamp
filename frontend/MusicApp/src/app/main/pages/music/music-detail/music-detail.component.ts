import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import Album from "app/model/album.model";
import Music from "app/model/music.model";
import { MusicService } from "app/services/music.service";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-music-detail",
    templateUrl: "./music-detail.component.html",
    styleUrls: ["./music-detail.component.scss"],
})
export class MusicDetailComponent implements OnInit {
    private musicId: string;

    album: Album;
    music: Music[];

    constructor(
        private router: Router,
        private service: MusicService,
        private route: ActivatedRoute
    ) {}

    ngOnInit() {
        this.musicId = this.route.snapshot.paramMap.get("id");

        let albumRequest = this.service.getAlbunsDetail(this.musicId);
        let musicRequest = this.service.getMusic(this.musicId);

        forkJoin([albumRequest, musicRequest]).subscribe((request) => {
            this.album = request[0];
            this.music = request[1];
        });
    }
    getMusicDuration(value: number) {
        const minutes: number = Math.floor(value / 60);
        return `${minutes.toString().padStart(2, "0")}:${(value - minutes * 60)
            .toString()
            .padStart(2, "0")}`;
    }
    back() {
        this.router.navigate(["page", "music"]);
    }
}

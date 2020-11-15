import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import Album from "app/model/album.model";
import Music from "app/model/music.model";
import User from "app/model/user.model";
import { MusicService } from "app/services/music.service";
import { PersistenceService } from "app/services/persistence.service";
import { UserService } from "app/services/user.service";
import { forkJoin } from "rxjs";
import Swal from "sweetalert2";

@Component({
    selector: "app-music-detail",
    templateUrl: "./music-detail.component.html",
    styleUrls: ["./music-detail.component.scss"],
})
export class MusicDetailComponent implements OnInit {
    private musicId: string;

    album: Album;
    music: Music[];
    user: User;

    constructor(
        private router: Router,
        private service: MusicService,
        private route: ActivatedRoute,
        private persistence: PersistenceService,
        private userService: UserService
    ) {}

    ngOnInit() {
        this.musicId = this.route.snapshot.paramMap.get("id");

        let albumRequest = this.service.getAlbunsDetail(this.musicId);
        let musicRequest = this.service.getMusic(this.musicId);

        forkJoin([albumRequest, musicRequest]).subscribe((request) => {
            this.album = request[0];
            this.music = request[1];
        });

        this.user = this.persistence.get("authenticate_user");
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
    toogleMusicFavorite(musicId) {
        if (this.isFavoriteMusic(musicId) === false) {
            this.userService
                .addMusicToFavorite(this.user.id, musicId)
                .subscribe((data) => {
                    this.persistence.set("authenticate_user", data);
                    this.user = data;
                    Swal.fire(
                        "Sucesso!",
                        "Musica adicionada ao favoritos",
                        "success"
                    );
                });
        } else {
            this.userService
                .removeMusicFromFavorite(this.user.id, musicId)
                .subscribe((data) => {
                    this.persistence.set("authenticate_user", data);
                    this.user = data;
                    Swal.fire(
                        "Sucesso!",
                        "Musica removida dos favoritos",
                        "success"
                    );
                });
        }
    }

    isFavoriteMusic(musicId) {
        return (
            this.user.favoriteMusics.findIndex((x) => x.musicId == musicId) !=
            -1
        );
    }
}

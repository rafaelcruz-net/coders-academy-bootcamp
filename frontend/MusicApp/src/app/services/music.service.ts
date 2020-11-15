import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import Album from "app/model/album.model";
import Music from "app/model/music.model";
import { environment } from "environments/environment";
import { Observable, of } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class MusicService {
    constructor(private http: HttpClient) {}

    public getAlbuns(): Observable<Album[]> {
        return this.http.get<Album[]>(`${environment.baseUrl}album`);
    }

    public getAlbunsDetail(id: string): Observable<Album> {
        return this.http.get<Album>(`${environment.baseUrl}album/${id}`);
    }

    public getMusic(id: string): Observable<Music[]> {
        return this.http.get<Music[]>(
            `${environment.baseUrl}album/${id}/music`
        );
    }
}

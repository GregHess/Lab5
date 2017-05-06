import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router'

@Component({
    selector: 'album',
    templateUrl: './album.component.html'
})
export class AlbumComponent {
    public album: Album;

    constructor(http: Http, route: ActivatedRoute) {
        var id = route.snapshot.params['id'];
        http.get('/api/AlbumsAPI/' + id).subscribe(result => {
            this.album = result.json();
        });
    }
}

interface Album {
    albumID: number;
    title: string;
    artist: Object;
    genre: Object;
    averageRating: number
}
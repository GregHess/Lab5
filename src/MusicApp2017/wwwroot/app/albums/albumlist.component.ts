import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'albumlist',
    templateUrl: './albumlist.component.html'
})
export class AlbumListComponent {
    public albums: Album[];

    constructor(http: Http) {
        http.get('/api/AlbumsAPI').subscribe(result => {
            this.albums = result.json();

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
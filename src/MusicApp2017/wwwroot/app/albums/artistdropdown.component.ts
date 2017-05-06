import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'artistdropdown',
    templateUrl: './artistdropdown.component.html'

})

export class ArtistDropdownComponent {
    public artists: Artist[];

    constructor(http: Http) {
        http.get('/api/ArtistsAPI').subscribe(result => {
            this.artists = result.json();
        });
    }
}

interface Artist {
    artistID: number;
    name: string;
    bio: string
}
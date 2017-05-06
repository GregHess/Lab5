import { Component } from '@angular/core';
import { Http} from '@angular/http';

@Component({
    selector: 'weather',
    templateUrl: './weather.component.html'
  //  providers: [Http]
})
export class WeatherComponent {
    public weather: Weather;

    constructor(http: Http) {
        http.get('/api/weather/city/Philly').subscribe(result => {
            this.weather = result.json();
        });
    }
}

interface Weather {
    temp: string;
    summary: string;
    city: string;
}
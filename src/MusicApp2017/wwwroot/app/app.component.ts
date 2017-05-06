import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
    styles: [`
  h1 {
     color: blue; 
  }`]
})

export class AppComponent  { 
  name = 'Angular 4'; 
}

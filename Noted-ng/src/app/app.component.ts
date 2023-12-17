import {Component, ElementRef, Renderer2, ViewChild} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'Noted-ng';

  constructor(private renderer: Renderer2) {
  }

  ngOnInit(){
  }

  OpenPage(s: string) {
    window.open(s,'_blank');
  }
}

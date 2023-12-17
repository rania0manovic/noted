import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
  constructor(private httpClient:HttpClient) {
  }
  ngOnInit():void{
  }

}

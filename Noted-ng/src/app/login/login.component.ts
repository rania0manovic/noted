import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Route, Router} from "@angular/router";
import {DataService} from "../SharedData/data.service";
import {SpinnerScreenComponent} from "../reusable-components/spinner-screen/spinner-screen.component";
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  email: any;
  password: any;
  wrongInput = false;
  errorMessage:any;
  user: any = {
    token: ""
  }
  isWaiting=false;

  constructor(private httpClient: HttpClient, private router: Router, private dataService: DataService) {
  }

  ngOnInit(): void {

    localStorage.removeItem('UserID');
    if (localStorage.getItem('UserID') != null)
      this.router.navigateByUrl("/home");

  }

  async VerifyLogin() {
    this.isWaiting=true;
     setTimeout(async () => {
       const x = await this.httpClient
         .get("https://localhost:7174/User/Login?email=" + this.email + "&password=" + this.password).toPromise();
       if (x != null) {
         this.user = x;
         if (this.user.token == null || this.user.errorMessage != null) {
           this.wrongInput = true;
           this.errorMessage = this.user.errorMessage;
           this.isWaiting = false;

         } else {
           this.wrongInput = false;
           localStorage.setItem('token', this.user.token);
           await this.router.navigateByUrl("/home");
           this.isWaiting = false;

         }
       }
     },500);
  }
}


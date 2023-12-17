import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  newUser:any;
  alert=false;
  errors:any= {
    firstName: null,
    lastName: null,
    email: null,
    password: null
  };
  isWaiting=false;
  constructor(private httpClient: HttpClient,public router: Router) {
  }
  ngOnInit():void{
    this.ClearData();
  }
   ClearData() {
    this.newUser={
      name:"",
      surname:"",
      password:"",
      email:"",
      phone:"",
      username:""
     }
  }
  TestName(input: any) {
    if( !/^[a-zA-Z]+$/.test(input)) {
      this.errors.name = "Name is required and must contain only letters."
      return false;
    }
    else{
      this.errors.name = null;
      return true;
    }
  }
  TestSurname(input: any) {
    if( !/^[a-zA-Z]+$/.test(input)) {
      this.errors.surname = "Surname is required and must contain only letters."
      return false;
    }
    else{
      this.errors.surname = null;
      return true;
    }
  }
  TestEmail(input: any) {
    if( !/^[\w\.-]+@[\w\.-]+\.\w+$/i.test(input)) {
      this.errors.email = "Email is required (format: johndoe@gmail.com)."
      return false;
    }
    else{
      this.errors.email = null;
      return true;
    }
  }
  TestPassword(input: any) {
    if( !/^.{8,}$/.test(input)) {
      this.errors.password = "Password is required and must have at least 8 characters."
      return false;
    }
    else{
      this.errors.password = null;
      return true;
    }
  }
  async Register() {
    this.TestName(this.newUser.name);
    this.TestSurname(this.newUser.surname);
    this.TestEmail(this.newUser.email);
    this.TestPassword(this.newUser.password);
    if (this.errors.name == null && this.errors.surname == null && this.errors.email == null && this.errors.password == null) {
      this.isWaiting = true;
      await this.httpClient.post('https://localhost:7174/User/Register', this.newUser)
        .toPromise();
      this.ClearData()
      this.isWaiting = false;
      this.alert = true;
    } else
      return;
  }
   CloseAlert(){
    this.alert=false;
     this.router.navigateByUrl("/home");
   }

}

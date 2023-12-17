import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {ProgressSpinnerMode} from '@angular/material/progress-spinner';
import {ThemePalette} from '@angular/material/core';
import jwt_decode from "jwt-decode";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  editing=false;
  isSuccess:any=false;
  user:any;
  selectedFile: File | null | any = null;
  selectedImageUrl: string | ArrayBuffer | null  = null;
  constructor(private router:Router, private httpClient: HttpClient) {

  }
  ngOnInit(){
  this.fetchUser().then();
}

  private async fetchUser() {
    let token = localStorage.getItem("token");
    if (token) {
      this.user = jwt_decode(token);
      const x = await this.httpClient.get('https://localhost:7174/api/Photos/GetProfilePhoto?userId=' + this.user.Id).toPromise();
      this.selectedFile = x;
      const base64ImageData = this.selectedFile.fileContents;
      const dataURL = `data:${this.selectedFile.contentType};base64,${base64ImageData}`
      this.selectedImageUrl = dataURL;
    } else
      await this.router.navigateByUrl("/login");
  }

  async EditUser() {
    if (this.editing) {
      let u = {
        id: this.user.Id,
        name: this.user.FirstName,
        surname: this.user.LastName,
        email: this.user.Email
      }
      const x = await this.httpClient.post('https://localhost:7174/User/Edit', u).toPromise();
      if (x != null) {
        this.isSuccess = true;
        let u: any = x;
        localStorage.setItem('token', u.token);
        await this.fetchUser();
      }
      setTimeout(() => {
        this.isSuccess = false;
      }, 6000);
    }
  }

  async onFileSelected(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.selectedFile = inputElement.files[0] as File;
      const formData = new FormData();
      formData.append("profilePhoto", this.selectedFile);
      await this.httpClient.post('https://localhost:7174/User/EditProfilePhoto?userId=' + this.user.Id, formData).toPromise();
      this.isSuccess = true;
      this.displaySelectedImage();
      setTimeout(() => {
        this.isSuccess = false;
      }, 6000);
    }
  }
  displaySelectedImage() {
    if (this.selectedFile) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.selectedImageUrl = e.target.result;
        console.log(this.selectedImageUrl);
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
}

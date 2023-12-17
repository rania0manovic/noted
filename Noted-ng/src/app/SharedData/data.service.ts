import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  userID:any;

  constructor() { }
  SetData(value:any){
    this.userID = value;
    localStorage.setItem('UserID',this.userID);
  }
  GetData(){
    return this.userID;
  }
}

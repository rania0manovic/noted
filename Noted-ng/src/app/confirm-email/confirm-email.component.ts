import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent {
   response :any="Please wait...";
   paramValue:any;
   constructor(private httpClient: HttpClient,
               private route: ActivatedRoute) {
   }
  ngOnInit(): void {
     this.Verify().then(x=>{});
  }

 async Verify() {
    this.route.queryParams.subscribe(p => {
       this.paramValue = p['token'];
     });
   const x = await this.httpClient.get("https://localhost:7174/api/ConfirmEmailRequests?token="+ this.paramValue.toString()).toPromise();
      this.response=x;
  }
}

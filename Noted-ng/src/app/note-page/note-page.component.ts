import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-note-page',
  templateUrl: './note-page.component.html',
  styleUrls: ['./note-page.component.css']
})
export class NotePageComponent {
  note_name: any;
  colors:any=[];

  id:any;
  note_data:any;
  title:any;
  constructor(private route:ActivatedRoute,
              private httpClient: HttpClient) {
  }
  ngOnInit():void{
   this.getData().then(x=>{});
  }
  async getData(){
    await this.GetNoteData();
    await this.GetColors();
  }
  async GetColors() {
    const x = await this.httpClient.get(`https://localhost:7174/api/Colors/GetAll`).toPromise();
    this.colors = x;
  }
  async ChangeColor(color: string) {
    color = color.substring(1);
    await this.httpClient.post(`https://localhost:7174/Table/EditColor?tableId=${this.note_data[0].id}&color=${color}`, null).toPromise();
  }
   async GetNoteData() {
     this.route.params.subscribe(p => {
       this.note_name = p['note'];
     });
     this.id = localStorage.getItem("ActiveTableID");
     this.id = parseInt(this.id)
     const x = await this.httpClient.get("https://localhost:7174/api/Note/noteId?id=" + this.id).toPromise();
     this.note_data = x;

   }

  async EditNote() {
    console.log(this.note_data.title)
    let obj = {
      id: this.id,
      title: this.note_data.title,
      data: this.note_data.data
    }
    await this.httpClient.post("https://localhost:7174/api/Note/Edit", obj).toPromise();
  }
}

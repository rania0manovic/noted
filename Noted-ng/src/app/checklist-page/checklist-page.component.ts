import { Component } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-checklist-page',
  templateUrl: './checklist-page.component.html',
  styleUrls: ['./checklist-page.component.css']
})
export class ChecklistPageComponent {
  checklist_name: any;
  checklist_data: any=null;
  id: any;
  DarkMode = false;
  colors:any=[];

  constructor(private route: ActivatedRoute,
              private httpClient: HttpClient,
              private router:Router) {
  }

  ngOnInit(): void {
   this.getData().then(r => {});
  }
  async  getData() {
    await this.GetChecklistData();
    await this.GetColors();
  }
  async GetChecklistData() {
    this.route.params.subscribe(p => {
      this.checklist_name = p['table'];
    });
    this.id = localStorage.getItem("ActiveTableID");
    this.id = parseInt(this.id)
    const x = await this.httpClient.get("https://localhost:7174/api/Checklist?id=" + this.id).toPromise();
      this.checklist_data = x;
      let style ='.checkbox-container input[type="checkbox"]:checked + .checkmark {\n' +
        '  background-color:'+  this.checklist_data.color.toString() +'!important;\n' +
        '}' + '.cb-input:focus, .cb-input:active{\n' +
        '  border-bottom: 2px solid '+ this.checklist_data.color.toString() + ';\n' +
        '}';
      let css = document.createElement('style');
      css.id = 'sty';
      css.innerText = style;
      let head = document.getElementById('head');
      head?.appendChild(css);

  }

  async EditChecklistItem(ci:any) {
    ci.checked= !ci.checked;
    await this.httpClient.post("https://localhost:7174/api/ChecklistItems/Edit", ci).toPromise();
  }

  checkLength(event: Event) {
    if ((event.target as HTMLElement).innerText.length >= 20) {
      (event.target as HTMLElement).innerText = (event.target as HTMLElement).innerText.substring(0, 20);
      const range = document.createRange();
      range.selectNodeContents((event.target as HTMLTableCellElement));
      range.collapse(false);
      const selection: any = window.getSelection();
      selection.removeAllRanges();
      selection.addRange(range);

    }
  }

  async AddNewChecklistItem() {
    let obj = {
      checklistId: this.id,
      description : "New Task Text"
    };
    await this.httpClient.post("https://localhost:7174/api/ChecklistItems/", obj).toPromise();
    await this.GetChecklistData();
  }

  async DeleteChecklist() {
   await this.httpClient.get(`https://localhost:7174/api/Checklist/Delete?id=${this.id}`).toPromise();
   await this.router.navigateByUrl('/home');
  }

  async DeleteChecklistItem(id:any) {
   await this.httpClient.get(`https://localhost:7174/api/ChecklistItems/Delete?id=${id}`).toPromise();
   await this.GetChecklistData();
  }


  async GetColors() {
    const x = await this.httpClient.get(`https://localhost:7174/api/Colors/GetAll`).toPromise();
      this.colors=x;
      this.colors.shift();
  }
  async Edit(ci: any, event:FocusEvent) {
    let name = (event.target as HTMLInputElement).value;
    ci.description= name;
    await this.httpClient.post(`https://localhost:7174/api/ChecklistItems/Edit`,ci).toPromise();
  }
  async ChangeColor(color:string) {
    color =color.substring(1);
    await this.httpClient.post(`https://localhost:7174/api/Checklist/EditColor?id=${this.id}&color=${color}`,null).toPromise();
    await this.GetChecklistData();
  }


}

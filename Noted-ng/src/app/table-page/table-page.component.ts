import {Component} from '@angular/core';
import {ActivatedRoute, Route, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent {
  table_name: any;
  table_data: any;
  id: any;
  columns: any;
  DarkMode = false;
  colors:any=[];

  constructor(private route: ActivatedRoute,
              private httpClient: HttpClient,
              private router:Router) {
  }

  ngOnInit(): void {
    this.getData().then(r => {});
  }
async getData (){
  await this.GetTableData();
  await this.GetColors();
}
  async GetTableData() {
    this.route.params.subscribe(p => {
      this.table_name = p['table'];
    });
    this.id = localStorage.getItem("ActiveTableID");
    this.id = parseInt(this.id)
    const x = await this.httpClient.get("https://localhost:7174/Table/GetFull/tableId?id=" + this.id).toPromise();
    this.table_data = x;
    this.columns = this.table_data[0].numberOfColumns;
  }

  async EditTableData(id: any, event: FocusEvent) {
    let obj = {
      dataId: id,
      data: (event.target as HTMLElement).innerText
    };
    await this.httpClient.post("https://localhost:7174/api/TableRowData/Edit", obj).toPromise();
  }

  checkLength(event: Event) {
    if ((event.target as HTMLElement).innerText.length >= 30) {
      (event.target as HTMLElement).innerText = (event.target as HTMLElement).innerText.substring(0, 20);
      const range = document.createRange();
      range.selectNodeContents((event.target as HTMLTableCellElement));
      range.collapse(false);
      const selection: any = window.getSelection();
      selection.removeAllRanges();
      selection.addRange(range);

    }
  }

  async AddNewRow() {
    let obj = {
      tableId: this.id,
      columns: this.columns
    };
    await this.httpClient.post("https://localhost:7174/TableRow/Add", obj).toPromise();
    await this.GetTableData();
  }

  async DeleteRow(id: any) {
    await this.httpClient.get(`https://localhost:7174/TableRow/Delete?rowId=${id}`).toPromise();
    await this.GetTableData();
  }

  async DeleteTable() {
    await this.httpClient.get(`https://localhost:7174/Table/Delete?tableId=${this.id}`).toPromise();
    await this.router.navigateByUrl('/home');
  }


   async GetColors() {
     const x = await this.httpClient.get(`https://localhost:7174/api/Colors/GetAll`).toPromise();
     this.colors = x;
   }
  async ChangeName(event: FocusEvent) {
    let name = (event.target as HTMLElement).innerText;
    await this.httpClient.post(`https://localhost:7174/Table/EditName?tableId=${this.table_data[0].id}&name=${name}`, null).toPromise();
  }
  async ChangeColor(color: string) {
    color = color.substring(1);
    await this.httpClient.post(`https://localhost:7174/Table/EditColor?tableId=${this.table_data[0].id}&color=${color}`, null).toPromise();
    await this.GetTableData();
  }
}

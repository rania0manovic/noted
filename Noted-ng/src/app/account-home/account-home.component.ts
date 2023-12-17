import {Component, ElementRef, Renderer2} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DataService} from "../SharedData/data.service";
import {Router} from "@angular/router";
import jwt_decode from "jwt-decode";
import {parse} from "@angular/compiler-cli/linker/babel/src/babel_core";

@Component({
  selector: 'app-account-home',
  templateUrl: './account-home.component.html',
  styleUrls: ['./account-home.component.css']
})
export class AccountHomeComponent {
  user: any;
  isHidden = true;
  quote: any;
  profileIsClicked = false;
  makeNewRepo = false;
  addNewItemToRepo = false;
  selectedFile: File | null | any = null;
  selectedImageUrl: string | ArrayBuffer | null  = null;
  selectedHeaderUrl: string | ArrayBuffer | null  = null;
  editRepo = false;
  deleteRepo = false;
  repoIsActive = false;
  addTableWindow = false;
  addCheckListWindow= false;
  user_repositories: any;
  new_repo: any;
  new_table: any;
  wrongInput = false;
  DarkMode = false;
  tables: any = [];
  notes:any=[];
  checklists:any=[];
  addNoteWindow=false;
  new_checklist:any;
  new_note:any;
  isWaiting=false;
  isSuccess=false;
  notifyMessage:any;


  constructor(private httpClient: HttpClient,
              private dataService: DataService,
              private router: Router,
              private elementRef: ElementRef,
              private renderer: Renderer2,) {
  }

  ngOnInit(): void {
    this.isWaiting=true;
    this.loadData().then(v=> this.isWaiting=false);
    this.ClearData();

  }
  async loadData (){
    await this.getUser();
    await this.GetMessage();
    await this.GetRepos();
  }

  ClearData() {
    this.new_repo = {
      name: "",
      userID: this.user.Id
    }
    this.new_table = {
      name: "",
      repositoryId: 0,
      numberOfColumns: 2
    }
    this.new_note = {
      name: "",
      repositoryId: 0,
    }
    this.new_checklist = {
      name: "",
      repositoryId: 0,
    }
  }

 async GetRepos() {
   const x = await this.httpClient.get('https://localhost:7174/Repository/GetAll?id=' + this.user.Id).toPromise();
      this.user_repositories = x;
  }

  async GetMessage() {
   const x = await this.httpClient.get('https://localhost:7174/Quote/GetDaily?id=' + this.user.Id).toPromise();
      let o: any = x;
      let a: any = {
        'userID': this.user.Id,
        'text': '',
        'resetDate': Date,
      }
      if (x == null || this.CheckTime(new Date(o.resetDate))) {
        const y = await this.httpClient.get('https://localhost:7174/Quote/Get').toPromise();
          let q: any = y;
          a.text = q.text;
          a.resetDate = this.GenerateTomorrowDate();
          const z = await this.httpClient.post('https://localhost:7174/Quote/AddDaily', a).toPromise()
            this.quote = a.text;
      } else
        this.quote = o.text;


  }

  GenerateTomorrowDate() {
    let now = new Date();
    now.setDate(now.getDate() + 1)
    now.setHours(6, 0, 0, 0);
    return now;
  }

  CheckTime(resetDate: Date) {
    let now = new Date();
    console.log(now > resetDate);
    return now > resetDate;

  }

  ShowHideList(name: string) {
    let el = document.querySelector(name) as HTMLElement;
    let nav = document.querySelector(".navbar-items") as HTMLElement;
    if (el != null) {
      if (this.isHidden) {
        el.style.display = "block";
        nav.className= nav.className +  " active";
        this.isHidden = false;
      } else {
        el.style.display = "none";
        nav.className = "navbar-items";
        this.isHidden = true;

      }
    }
  }

  async getUser() {
    let token = localStorage.getItem("token");
    if (token) {
      this.user = jwt_decode(token);
     try {
       const x = await this.httpClient.get('https://localhost:7174/api/Photos/GetProfilePhoto?userId=' + this.user.Id).toPromise();
       this.selectedFile = x;
       const base64ImageData = this.selectedFile.fileContents;
       const profilePhotourl = `data:${this.selectedFile.contentType};base64,${base64ImageData}`
       this.selectedImageUrl= profilePhotourl;
     }
     catch (e){
     }
      try {
        const y = await this.httpClient.get('https://localhost:7174/api/Photos/GetHeaderPhoto?userId=' + this.user.Id).toPromise();
        this.selectedFile = y;
        const base64ImageData1 = this.selectedFile.fileContents;
        const headerurl = `data:${this.selectedFile.contentType};base64,${base64ImageData1}`
        this.selectedHeaderUrl= headerurl;
      }
      catch (e) {
      }
    }
    else
     await this.router.navigateByUrl("/sign-in");
  }

  ProfileOnClick() {
    this.profileIsClicked = !this.profileIsClicked;
  }

  SignOut() {
    localStorage.removeItem("token");
    this.router.navigateByUrl("");
  }

  CloseWindows() {
    this.profileIsClicked = false;
  }

  ClickedNewRepo() {
    this.makeNewRepo = true;
    this.ClearData();
    this.repoIsActive = false;
  }

  CloseRepo() {
    this.makeNewRepo = false;
    this.editRepo = false;
    this.deleteRepo = false;
    this.wrongInput = false;
  }

  async AddNewRepo() {
    if (this.TestName(this.new_repo.name) && this.new_repo.name != '') {
      this.new_repo.userID = parseInt(this.new_repo.userID);
      await this.httpClient.post("https://localhost:7174/Repository/Add", this.new_repo).toPromise();
        this.CloseRepo();
        this.GetRepos();
        this.notifyMessage="You have successfully created a new repository: " + this.new_repo.name;
        this.isSuccess=true;

      setTimeout(()=>{
        this.isSuccess=false;
      },6000);
    } else {
      this.wrongInput = true;
    }
  }

  TestName(name: any) {
    return /^[a-zA-Z0-9-]*$/.test(name);
  }

  RepoOnClick(id: any, name: any) {
    localStorage.setItem('ActiveRepositoryID', id);
    this.repoIsActive = true;
    this.new_repo.name = name;
    this.FetchTables(id);
    this.FetchNotes(id);
    this.FetchChecklists(id);
  }

  async EditRepo() {
    if (this.TestName(this.new_repo.name) && this.new_repo.name != '') {
      await this.httpClient.post('https://localhost:7174/Repository/Edit?id=' + localStorage.getItem('ActiveRepositoryID'), this.new_repo).toPromise();
        this.CloseRepo();
        await this.GetRepos();
    } else {
      this.wrongInput = true;
    }
  }

  async DeleteRepo() {
    await this.httpClient.post('https://localhost:7174/Repository/Delete?id=' + localStorage.getItem('ActiveRepositoryID'), null).toPromise();
      this.CloseRepo();
      await this.GetRepos();
      this.repoIsActive = false;
  }


  async FetchTables(id: any) {
    const x = await this.httpClient.get("https://localhost:7174/Table/Get/repositoryId?id=" + id).toPromise();
      if (x != null) {
        this.tables = x;
      }
  }

 async  AddNewTable() {
    let repo: any = localStorage.getItem('ActiveRepositoryID');
    this.new_table.repositoryId = parseInt(repo);
    if (this.TestName(this.new_table.name) && this.new_table.name != '') {
     const x = await  this.httpClient.post('https://localhost:7174/Table/AddInitial', this.new_table).toPromise();
        if (x != null) {
          this.wrongInput = false;
          this.addTableWindow = false;
          this.addNewItemToRepo = false;
         await this.FetchTables(repo);
          this.notifyMessage="You have successfully created a new table: " + this.new_table.name;
          this.isSuccess=true;
        }
      setTimeout(()=>{
        this.isSuccess=false;
      },6000);
    } else {
      this.wrongInput = true;
    }

  }



  NavigateToSelectedTable(tName: any, id: any) {
    localStorage.setItem('ActiveTableID', id);
    this.router.navigateByUrl(`/home/table/${tName}`);
  }

  async AddNewNote() {
    let repo: any = localStorage.getItem('ActiveRepositoryID');
    this.new_note.repositoryId = parseInt(repo);
    if(this.TestName(this.new_note.name) && this.new_note.name != ''){
     await this.httpClient.post("https://localhost:7174/api/Note", this.new_note).toPromise();
        await this.FetchNotes(this.new_note.repositoryId);
        this.addNoteWindow=false;
        this.wrongInput=false;
        this.notifyMessage="You have successfully created a new note: " + this.new_note.name;
        this.isSuccess=true;
    }
    else {
      this.wrongInput=true;
    }
    setTimeout(()=>{
      this.isSuccess=false;
    },6000);
  }
  async AddNewChecklist() {
    let repo: any = localStorage.getItem('ActiveRepositoryID');
    this.new_checklist.repositoryId = parseInt(repo);
    if(this.TestName(this.new_checklist.name) && this.new_checklist.name != ''){
      await this.httpClient.post("https://localhost:7174/api/Checklist", this.new_checklist).toPromise();
        await this.FetchChecklists(this.new_checklist.repositoryId);
        this.addCheckListWindow=false;
        this.wrongInput=false;
        this.notifyMessage="You have successfully created a new checklist: " + this.new_checklist.name;
        this.isSuccess=true;
    }
    else {
      this.wrongInput=true;
    }
    setTimeout(()=>{
      this.isSuccess=false;
    },6000);
  }

  async FetchNotes(repositoryId: number) {
     const x  = await this.httpClient.get("https://localhost:7174/api/Note/repositoryId?id=" + repositoryId).toPromise();
       if (x != null) {
         this.notes = x;
       }
  }
  async FetchChecklists(repositoryId: number) {
    const x = await this.httpClient.get("https://localhost:7174/api/Checklist/repositoryId?id=" + repositoryId).toPromise();
      if (x != null) {
        this.checklists = x;
      }
  }
  NavigateToSelectedNote(tName: any, id: any) {
    localStorage.setItem('ActiveTableID', id);
    this.router.navigateByUrl(`/home/note/${tName}`);
  }
  NavigateToSelectedCheckList(cName: any, id: any) {
    localStorage.setItem('ActiveTableID', id);
    this.router.navigateByUrl(`/home/checklist/${cName}`);
  }
  async onFileSelected(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.selectedFile = inputElement.files[0] as File;
      const formData = new FormData();
      formData.append("profilePhoto", this.selectedFile);
     await this.httpClient.post('https://localhost:7174/User/EditHeaderPhoto?userId='+ this.user.Id,formData).toPromise();
        this.notifyMessage="You have successfully updated your header photo!"
        this.isSuccess=true;
      this.displaySelectedImage();
      setTimeout(()=>{
        this.isSuccess=false;
      },6000);
    }
  }
  displaySelectedImage() {
    if (this.selectedFile) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.selectedHeaderUrl = e.target.result;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
}

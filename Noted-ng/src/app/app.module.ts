import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {FormsModule} from "@angular/forms";
import {RouterOutlet,RouterModule} from "@angular/router";
import {HttpClientModule} from "@angular/common/http";
import {RegisterComponent} from "./register/register.component";
import { HomePageComponent } from './home-page/home-page.component';
import { AccountHomeComponent } from './account-home/account-home.component';
import { SettingsComponent } from './settings/settings.component';
import { ProfileComponent } from './profile/profile.component';
import { TablePageComponent } from './table-page/table-page.component';
import { NotePageComponent } from './note-page/note-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import { TestingComponent } from './testing/testing.component';
import {MatCardModule} from "@angular/material/card";
import {MatRadioModule} from "@angular/material/radio";
import {MatSliderModule} from "@angular/material/slider";
import { SpinnerScreenComponent } from './reusable-components/spinner-screen/spinner-screen.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import {AuthGuard} from "./auth.guard";
import { ButtonComponent } from './reusable-components/button-component/button.component';
import { WebglBackgroundComponent } from './webgl-background/webgl-background.component';
import { NotifyComponent } from './notify/notify.component';
import { ChecklistPageComponent } from './checklist-page/checklist-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomePageComponent,
    AccountHomeComponent,
    SettingsComponent,
    ProfileComponent,
    TablePageComponent,
    NotePageComponent,
    TestingComponent,
    SpinnerScreenComponent,
    ConfirmEmailComponent,
    ButtonComponent,
    WebglBackgroundComponent,
    NotifyComponent,
    ChecklistPageComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      [
        {path: '', component: HomePageComponent},
        {path: 'login', component: LoginComponent},
        {path: 'register', component: RegisterComponent},
        {path: 'home', canActivate:[AuthGuard], component: AccountHomeComponent},
        {path: 'settings', canActivate:[AuthGuard], component: SettingsComponent},
        {path: 'profile', canActivate:[AuthGuard], component: ProfileComponent},
        {path: 'home/table/:table',canActivate:[AuthGuard], component: TablePageComponent, pathMatch: 'prefix'},
        {path: 'home/note/:note',canActivate:[AuthGuard], component: NotePageComponent, pathMatch: 'prefix'},
        {path: 'home/checklist/:checklist',canActivate:[AuthGuard], component: ChecklistPageComponent, pathMatch: 'prefix'},
        {path: 'confirm-email', component: ConfirmEmailComponent, pathMatch: 'prefix'},
        {path: 'webgl', component: WebglBackgroundComponent, pathMatch: 'prefix'},

      ]
    ),
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatRadioModule,
    MatSliderModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {

}

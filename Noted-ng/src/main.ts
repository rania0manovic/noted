import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import {ViewChild} from "@angular/core";

import { AppModule } from './app/app.module';

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));

export class main{

}

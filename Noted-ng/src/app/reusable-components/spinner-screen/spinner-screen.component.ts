import {Component, Input} from '@angular/core';
import {ProgressSpinnerMode} from "@angular/material/progress-spinner";

@Component({
  selector: 'app-spinner-screen',
  templateUrl: './spinner-screen.component.html',
  styleUrls: ['./spinner-screen.component.css']
})
export class SpinnerScreenComponent {
  @Input() backgroundStyles: { [key: string]: string };
  @Input() spinnerStyles: { [key: string]: string };
  constructor() {
    this.backgroundStyles = {};
    this.spinnerStyles={};
  }
  mode: ProgressSpinnerMode = 'indeterminate';
  value = 100;
}

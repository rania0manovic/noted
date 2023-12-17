import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button-component',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent {
  @Input() buttonStyles: { [key: string]: string };
  constructor() {
    this.buttonStyles = {};
  }
}

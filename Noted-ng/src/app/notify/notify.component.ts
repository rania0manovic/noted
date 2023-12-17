import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-notify',
  templateUrl: './notify.component.html',
  styleUrls: ['./notify.component.css']
})
export class NotifyComponent {
  @Input() type: 'success' | 'error' = 'success';
  @Input() message: string = '';
  @Input() autoCloseDuration: number = 5000;
  progressWidth = '100%';

  ngOnInit(): void {
    const interval = 10;
    const totalSteps = this.autoCloseDuration / interval;
    let currentStep = 0;

    const progressBarUpdater = setInterval(() => {
      currentStep++;
      this.progressWidth = ((totalSteps - currentStep) / totalSteps) * 100 + '%';

      if (currentStep === totalSteps) {
        clearInterval(progressBarUpdater);
        this.closeNotif();
      }
    }, interval);
  }

  closeNotif(): void {
    const toastElement = document.querySelector('.notify-container');
    if (toastElement) {
      toastElement.remove();
    }
  }
}

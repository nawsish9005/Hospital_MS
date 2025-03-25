import { Component, Input, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.css']
})
export class PreloaderComponent implements OnDestroy{
  @Input() isLoading: boolean = true;
  private timeout: any;

  constructor() {
    this.timeout = setTimeout(() => {
      this.isLoading = false;
    }, 5000);
  }

  ngOnDestroy() {
    if (this.timeout) {
      clearTimeout(this.timeout);
    }
  }
}

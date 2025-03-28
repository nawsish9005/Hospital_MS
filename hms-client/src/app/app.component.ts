import { Component } from '@angular/core';
import { NavigationStart, Router, Event as NavigationEvent } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoading: boolean = true;

  constructor(private router: Router) {
    // Simulate loading completion after 3 seconds
    setTimeout(() => {
      this.isLoading = false;
    }, 3000);

    // Or use router events to show/hide during navigation
    this.router.events.subscribe((event: NavigationEvent) => {
      if (event instanceof NavigationStart) {
        this.isLoading = true;
      } else {
        setTimeout(() => {
          this.isLoading = false;
        }, 500);
      }
    });
  }
}

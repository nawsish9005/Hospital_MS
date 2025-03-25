import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoading: boolean = true;

  ngOnInit() {
    // Simulate loading completion
    setTimeout(() => {
      this.isLoading = false;
    }, 3000);
  }
}
import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit{
  appointments: any[] = [];
  constructor(private hmsService: HmsService) {}
  ngOnInit(): void {
    this.getAppointments();
  }
  getAppointments(): void {
    this.hmsService.GetAllAppointments().subscribe(data => {
      this.appointments = data as any[];
    });
  }
}

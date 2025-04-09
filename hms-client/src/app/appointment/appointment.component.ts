import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  appointment = {
    id: 0,
    doctorId: 0,
    patientId: 0,
    purpose: '',
    date: ''
  };

  appointments: any[] = [];
  doctors: any[] = [];
  patients: any[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getAppointments();
    this.getDoctors();
    this.getPatients();
  }

  getAppointments(): void {
    this.hmsService.GetAllAppointments().subscribe((data: any) => {
      this.appointments = data;
      
    });
  }
  getDoctors(): void {
    this.hmsService.GetDoctor().subscribe((data: any) => {
      this.doctors = data;
    });
  }

  getPatients(): void {
    this.hmsService.GetPatient().subscribe((data: any) => {
      this.patients = data;
    });
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.updateAppointment();
    } else {
      this.createAppointment();
    }
  }

  createAppointment(): void {
    this.hmsService.CreateAppointment(this.appointment).subscribe(
      () => {
        alert('Appointment created!');
        this.resetForm();
        this.getAppointments();
      },
      error => alert('Error creating appointment')
    );
  }

  updateAppointment(): void {
    if (!this.appointment || !this.appointment.id) {
      alert('Invalid appointment');
      return;
    }
  
    this.hmsService.UpdateAppointment(this.appointment.id, this.appointment).subscribe(
      () => {
        alert('Appointment updated!');
        this.resetForm();
        this.getAppointments();
      },
      error => {
        console.error('Error updating appointment:', error);
        alert('Error updating appointment');
      }
    );
  }
  

  editAppointment(appointment: any): void {
    this.appointment = {
      ...appointment,
      date: appointment.date?.split('T')[0]  // Convert "2025-03-30T00:00:00" to "2025-03-30"
    };
    this.isEditMode = true;
  }
  

  deleteAppointment(id: number): void {
    if (confirm('Delete this appointment?')) {
      this.hmsService.DeleteAppointment(id).subscribe(
        () => {
          alert('Deleted successfully!');
          this.getAppointments();
        },
        error => alert('Delete failed')
      );
    }
  }

  getDoctorName(doctorId: number): string {
    const doctor = this.doctors.find(d => d.id === doctorId);
    return doctor ? doctor.name : 'Unknown';
  }
  
  getPatientName(patientId: number): string {
    const patient = this.patients.find(p => p.id === patientId);
    return patient ? patient.name : 'Unknown';
  }

  resetForm(): void {
    this.appointment = {
      id: 0,
      doctorId: 0,
      patientId: 0,
      purpose: '',
      date: ''
    };
    this.isEditMode = false;
  }
}

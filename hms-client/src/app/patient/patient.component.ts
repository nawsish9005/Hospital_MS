import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit{
  patients: any[] = [];

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients(): void {
    this.hmsService.GetPatient().subscribe(data => {
      this.patients = data as any[];
    });
  }

  getPatientById(id: number): void {
    this.hmsService.GetPatientById(id).subscribe(data => {
      console.log(data);
    });
  }

  createPatient(patient: any): void {
    this.hmsService.CreatePatient(patient).subscribe(() => {
      this.getPatients();
    });
  }

  updatePatient(patient: any): void {
    this.hmsService.UpdatePatient(patient).subscribe(() => {
      this.getPatients();
    });
  }

  deletePatient(id: number): void {
    this.hmsService.DeletePatient(id).subscribe(() => {
      this.getPatients();
    });
  }
}

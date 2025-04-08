import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html'
})
export class PrescriptionComponent implements OnInit {
  prescription: any = {
    doctorId: null,
    patientId: null,
    duration: '',
    notes: '',
    medicineInfos: []
  };

  prescriptions: any[] = [];
  doctors: any[] = [];
  patients: any[] = [];

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPrescriptions();
    this.getDoctors();
    this.getPatients();
  }

  getPrescriptions(): void {
    this.hmsService.GetAllPrescriptions().subscribe((data: any) => {
      this.prescriptions = data;
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

  getDoctorName(doctorId: number): string {
    const doctor = this.doctors.find(d => d.id === doctorId);
    return doctor ? doctor.name : 'Unknown';
  }
  
  getPatientName(patientId: number): string {
    const patient = this.patients.find(p => p.id === patientId);
    return patient ? patient.name : 'Unknown';
  }
  

  addMedicine(): void {
    this.prescription.medicineInfos.push({ name: '' });
  }

  removeMedicine(index: number): void {
    this.prescription.medicineInfos.splice(index, 1);
  }

  onSubmit(): void {
    if (this.prescription.id) {
      this.hmsService.UpdatePrescription(this.prescription.id, this.prescription).subscribe(() => {
        alert('Prescription updated successfully!');
        this.resetForm();
        this.getPrescriptions();
      });
    } else {
      this.hmsService.CreatePrescription(this.prescription).subscribe(() => {
        alert('Prescription added successfully!');
        this.resetForm();
        this.getPrescriptions();
      });
    }
  }

  editPrescription(p: any): void {
    this.prescription = {
      ...p,
      medicineInfos: [...p.medicineInfos]
    };
  }

  deletePrescription(id: number): void {
    if (confirm('Are you sure you want to delete this prescription?')) {
      this.hmsService.DeletePrescription(id).subscribe(() => {
        alert('Prescription deleted successfully!');
        this.getPrescriptions();
      });
    }
  }

  resetForm(): void {
    this.prescription = {
      doctorId: null,
      patientId: null,
      duration: '',
      notes: '',
      medicineInfos: []
    };
  }
}

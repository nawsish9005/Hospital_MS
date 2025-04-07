import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit {
  patient = {  
    id: 0,
    name: '',
    contact: '',
    dateOfBirth: '',
    bloodGroup: '',
    email: '',
    address: '',
    region: '',
    country: '',
    imageUrls: ''
  };

  selectedFile: File | null = null;
  patients: Patient[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients(): void {
    this.hmsService.GetPatient().subscribe(
      data => {
        this.patients = data as Patient[];
        this.patients = this.patients.map(patient => ({
          ...patient,
          imageUrls: `https://localhost:7212${patient.imageUrls}`, 
          dateOfBirth: this.formatDate(patient.dateOfBirth)
          
        }));
        console.log(this.patients);
      },
      error => {
        console.error('Error fetching patients:', error);
        alert('Error fetching patient list!');
      }
    );
  }

  private formatDate(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toLocaleDateString(); // or your preferred format
  }

  getPatientById(id: number): void {
    this.hmsService.GetPatientById(id).subscribe(
      data => {
        this.patient = { ...data } as any;
        this.isEditMode = true;
      },
      error => {
        console.error('Error fetching patient:', error);
        alert('Error fetching patient details!');
      }
    );
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  createPatient(): void {
    const formData = new FormData();
    formData.append('name', this.patient.name);
    formData.append('contact', this.patient.contact);
    formData.append('email', this.patient.email);
    formData.append('address', this.patient.address);
    formData.append('region', this.patient.region || '');
    formData.append('country', this.patient.country);
    formData.append('bloodGroup', this.patient.bloodGroup || '');
    formData.append('dateOfBirth', this.patient.dateOfBirth);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.hmsService.CreatePatient(formData).subscribe(
      response => {
        console.log('Patient added:', response);
        alert('Patient added successfully!');
        this.resetForm();
        this.getPatients();
      },
      error => {
        console.error('Error adding patient:', error);
        alert('Failed to add patient. Please check your inputs.');
      }
    );
  }

  updatePatient(): void {
    if (this.patient.id === 0) {
      alert('Invalid Patient ID');
      return;
    }

    const formData = new FormData();
    formData.append('id', this.patient.id.toString());
    formData.append('name', this.patient.name);
    formData.append('contact', this.patient.contact);
    formData.append('email', this.patient.email);
    formData.append('address', this.patient.address);
    formData.append('region', this.patient.region || '');
    formData.append('country', this.patient.country);
    formData.append('bloodGroup', this.patient.bloodGroup || '');
    formData.append('dateOfBirth', this.patient.dateOfBirth);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.hmsService.UpdatePatient(this.patient.id, formData).subscribe(
      () => {
        console.log('Patient updated successfully!');
        alert('Patient updated successfully!');
        this.resetForm();
        this.getPatients();
      },
      error => {
        console.error('Error updating patient:', error);
        alert('Failed to update patient.');
      }
    );
  }

  deletePatient(id: number): void {
    if (confirm('Are you sure you want to delete this patient?')) {
      this.hmsService.DeletePatient(id).subscribe(
        () => {
          console.log('Patient deleted successfully!');
          alert('Patient deleted successfully!');
          this.getPatients();
        },
        error => {
          console.error('Error deleting patient:', error);
          alert('Failed to delete patient.');
        }
      );
    }
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.updatePatient();
    } else {
      this.createPatient();
    }
  }

  editRecord(record: any): void {
    this.patient = { ...record };
    this.isEditMode = true;
  }

  resetForm(): void {
    this.patient = {
      id: 0,
      name: '',
      contact: '',
      email: '',
      address: '',
      region: '',
      country: '',
      dateOfBirth: '',
      imageUrls: '',
      bloodGroup: ''
    };
    this.selectedFile = null;
    this.isEditMode = false;
  }
}

interface Patient {
  id: number;
  name: string;
  email: string;
  contact: string;
  address: string;
  country: string;
  region: string;
  postalCode: string;
  imageUrls: string;
  dateOfBirth: string;
  bloodGroup?: string;
  gender?: string;
}

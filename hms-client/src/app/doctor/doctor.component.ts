
import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {
  doctor = {  
    id: 0,
    name: '',
    contact: '',
    email: '',
    address: '',
    region: '',
    country: '',
    postalCode: '',
    imageUrl: '',
    specialization: ''
  };

  selectedFile: File | null = null;
  doctors: Doctor[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(): void {
    this.hmsService.GetDoctor().subscribe(
      data => {
        this.doctors = data as Doctor[];
        this.doctors = this.doctors.map(doctor => ({
          ...doctor,
          imageUrl: `https://localhost:7212${doctor.imageUrl}`    
      }));
      
      },
      error => {
        console.error('Error fetching doctors:', error);
        alert('Error fetching doctor list!');
      }
    );
  }

  getDoctorById(id: number): void {
    this.hmsService.GetDoctorById(id).subscribe(
      data => {
        this.doctor = { ...data } as any;
        this.isEditMode = true;
      },
      error => {
        console.error('Error fetching doctor:', error);
        alert('Error fetching doctor details!');
      }
    );
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  createDoctor(): void {
    const formData = new FormData();
    formData.append('name', this.doctor.name);
    formData.append('contact', this.doctor.contact);
    formData.append('email', this.doctor.email);
    formData.append('address', this.doctor.address);
    formData.append('region', this.doctor.region || '');
    formData.append('country', this.doctor.country);
    formData.append('postalCode', this.doctor.postalCode || '');
    formData.append('specialization', this.doctor.specialization);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.hmsService.CreateDoctor(formData).subscribe(
      response => {
        console.log('Doctor added:', response);
        alert('Doctor added successfully!');
        this.resetForm();
        this.getDoctors();
      },
      error => {
        console.error('Error adding doctor:', error);
        alert('Failed to add doctor. Please check your inputs.');
      }
    );
  }

  updateDoctor(): void {
    if (this.doctor.id === 0) {
      alert('Invalid Doctor ID');
      return;
    }

    const formData = new FormData();
    formData.append('id', this.doctor.id.toString());
    formData.append('name', this.doctor.name);
    formData.append('contact', this.doctor.contact);
    formData.append('email', this.doctor.email);
    formData.append('address', this.doctor.address);
    formData.append('region', this.doctor.region || '');
    formData.append('country', this.doctor.country);
    formData.append('postalCode', this.doctor.postalCode || '');
    formData.append('specialization', this.doctor.specialization);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.hmsService.UpdateDoctor(this.doctor.id, formData).subscribe(
      () => {
        console.log('Doctor updated successfully!');
        alert('Doctor updated successfully!');
        this.resetForm();
        this.getDoctors();
      },
      error => {
        console.error('Error updating doctor:', error);
        alert('Failed to update doctor.');
      }
    );
  }

  deleteDoctor(id: number): void {
    if (confirm('Are you sure you want to delete this doctor?')) {
      this.hmsService.DeleteDoctor(id).subscribe(
        () => {
          console.log('Doctor deleted successfully!');
          alert('Doctor deleted successfully!');
          this.getDoctors();
        },
        error => {
          console.error('Error deleting doctor:', error);
          alert('Failed to delete doctor.');
        }
      );
    }
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.updateDoctor();
    } else {
      this.createDoctor();
    }
  }

  resetForm(): void {
    this.doctor = {
      id: 0,
      name: '',
      contact: '',
      email: '',
      address: '',
      region: '',
      country: '',
      postalCode: '',
      imageUrl: '',
      specialization: ''
    };
    this.selectedFile = null;
    this.isEditMode = false;
  }

}

interface Doctor {
  id: number;
  name: string;
  email: string;
  contact: string;
  address: string;
  country: string;
  region: string;
  postalCode: string;
  imageUrl: string;
  specialization: string;
}
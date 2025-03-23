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

  doctors: any[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(): void {
    this.hmsService.GetDoctor().subscribe(data => {
      this.doctors = data as any[];
    });
  }

  getDoctorById(id: number): void {
    this.hmsService.GetDoctorById(id).subscribe(data => {
      this.doctor = { ...data } as any;
      this.isEditMode = true;
    });
  }

  createDoctor(): void {
    this.hmsService.CreateDoctor(this.doctor).subscribe(response => {
      console.log('Doctor added:', response);
      alert('Doctor added successfully!');
      this.resetForm();
      this.getDoctors();
    });
  }

  updateDoctor(): void {
    if (this.doctor.id === 0) {
      alert('Invalid Doctor ID');
      return;
    }
    
    this.hmsService.UpdateDoctor(this.doctor.id).subscribe(() => {
      console.log('Doctor updated successfully!');
      alert('Doctor updated successfully!');
      this.resetForm();
      this.getDoctors();
    });
  }

  deleteDoctor(id: number): void {
    if (confirm('Are you sure you want to delete this doctor?')) {
      this.hmsService.DeleteDoctor(id).subscribe(() => {
        console.log('Doctor deleted successfully!');
        alert('Doctor deleted successfully!');
        this.getDoctors();
      });
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
    this.isEditMode = false;
  }
}

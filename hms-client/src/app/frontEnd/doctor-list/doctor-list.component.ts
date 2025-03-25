import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/app/models/doctor.model';
import { HmsService } from 'src/app/services/hms.service';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];
  isLoading: boolean = true;
  errorMessage: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalItems: number = 0;
  searchTerm: string = '';

  constructor(private hmsService: HmsService) { }

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.isLoading = true;
    this.errorMessage = '';
    
    this.hmsService.GetDoctor().subscribe({
      next: (data) => {
        this.doctors = data as Doctor[];
        this.totalItems = this.doctors.length;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error fetching doctors:', error);
        this.errorMessage = 'Failed to load doctors. Please try again later.';
        this.isLoading = false;
      }
    });
  }

  deleteDoctor(id: number): void {
    if (confirm('Are you sure you want to delete this doctor?')) {
      this.hmsService.DeleteDoctor(id).subscribe({
        next: () => {
          this.doctors = this.doctors.filter(doctor => doctor.id !== id);
          this.totalItems = this.doctors.length;
        },
        error: (error) => {
          console.error('Error deleting doctor:', error);
          alert('Failed to delete doctor. Please try again.');
        }
      });
    }
  }

  get filteredDoctors(): Doctor[] {
    if (!this.searchTerm) {
      return this.doctors;
    }
    return this.doctors.filter(doctor =>
      doctor.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      doctor.specialization.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      doctor.contact.includes(this.searchTerm) ||
      doctor.email.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  get paginatedDoctors(): Doctor[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredDoctors.slice(startIndex, startIndex + this.itemsPerPage);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
  }

  get totalPages(): number {
    return Math.ceil(this.filteredDoctors.length / this.itemsPerPage);
  }
}

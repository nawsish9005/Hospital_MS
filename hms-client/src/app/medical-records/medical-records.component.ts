import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-medical-records',
  templateUrl: './medical-records.component.html',
  styleUrls: ['./medical-records.component.css']
})
export class MedicalRecordsComponent implements OnInit {
  medicalRecord = {
    id: 0,
    patientId: 0,
    diagnosis: '',
    treatment: '',
    currentMedications: '',
    consultationNotes: '',
    followUpNotes: '',
    laboratoryResults: '',
    therapies: '',
    nextVisitDate: '',
    paymentRecords: '',
    imageUrls: ''
  };
  patients: any[] = [];
  selectedFiles: File[] = [];
  medicalRecords: any[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getAllMedicalRecords();
  }

  onFileSelected(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFiles = event.target.files[0];
    }
  }
  
  getAllMedicalRecords(): void {
    this.hmsService.GetAllMedicalRecords().subscribe(
      data => {
        this.medicalRecords = data as any[];
      },
      error => {
        console.error('Error fetching medical records:', error);
        alert('Error fetching medical records list!');
      }
    );
  }

  getMedicalRecordById(id: number): void {
    this.hmsService.GetMedicalRecordById(id).subscribe(
      data => {
        this.medicalRecord = { ...data } as any;
        this.isEditMode = true;
      },
      error => {
        console.error('Error fetching medical record:', error);
        alert('Error fetching medical record details!');
      }
    );
  }

  onFilesSelected(event: any): void {
    this.selectedFiles = Array.from(event.target.files);
  }

  createMedicalRecord(): void {
    const formData = new FormData();
    formData.append('patientId', this.medicalRecord.patientId.toString());
    formData.append('diagnosis', this.medicalRecord.diagnosis);
    formData.append('treatment', this.medicalRecord.treatment);
    formData.append('currentMedications', this.medicalRecord.currentMedications);
    formData.append('consultationNotes', this.medicalRecord.consultationNotes);
    formData.append('followUpNotes', this.medicalRecord.followUpNotes);
    formData.append('laboratoryResults', this.medicalRecord.laboratoryResults);
    formData.append('therapies', this.medicalRecord.therapies);
    formData.append('nextVisitDate', this.medicalRecord.nextVisitDate);
    formData.append('paymentRecords', this.medicalRecord.paymentRecords);

    if (this.selectedFiles.length > 0) {
      this.selectedFiles.forEach(file => {
        formData.append('images', file);
      });
    }

    this.hmsService.CreateMedicalRecord(formData).subscribe(
      response => {
        console.log('Medical record added:', response);
        alert('Medical record added successfully!');
        this.resetForm();
        this.getAllMedicalRecords();
      },
      error => {
        console.error('Error adding medical record:', error);
        alert('Failed to add medical record. Please check your inputs.');
      }
    );
  }

  updateMedicalRecord(): void {
    if (this.medicalRecord.id === 0) {
      alert('Invalid Medical Record ID');
      return;
    }

    const formData = new FormData();
    formData.append('id', this.medicalRecord.id.toString());
    formData.append('patientId', this.medicalRecord.patientId.toString());
    formData.append('diagnosis', this.medicalRecord.diagnosis);
    formData.append('treatment', this.medicalRecord.treatment);
    formData.append('currentMedications', this.medicalRecord.currentMedications);
    formData.append('consultationNotes', this.medicalRecord.consultationNotes);
    formData.append('followUpNotes', this.medicalRecord.followUpNotes);
    formData.append('laboratoryResults', this.medicalRecord.laboratoryResults);
    formData.append('therapies', this.medicalRecord.therapies);
    formData.append('nextVisitDate', this.medicalRecord.nextVisitDate);
    formData.append('paymentRecords', this.medicalRecord.paymentRecords);

    if (this.selectedFiles.length > 0) {
      this.selectedFiles.forEach(file => {
        formData.append('images', file);
      });
    }

    this.hmsService.UpdateMedicalRecord(this.medicalRecord.id, formData).subscribe(
      () => {
        console.log('Medical record updated successfully!');
        alert('Medical record updated successfully!');
        this.resetForm();
        this.getAllMedicalRecords();
      },
      error => {
        console.error('Error updating medical record:', error);
        alert('Failed to update medical record.');
      }
    );
  }

  deleteMedicalRecord(id: number): void {
    if (confirm('Are you sure you want to delete this medical record?')) {
      this.hmsService.DeleteMedicalRecord(id).subscribe(
        () => {
          console.log('Medical record deleted successfully!');
          alert('Medical record deleted successfully!');
          this.getAllMedicalRecords();
        },
        error => {
          console.error('Error deleting medical record:', error);
          alert('Failed to delete medical record.');
        }
      );
    }
  }

  getPatients(): void {
    this.hmsService.GetPatient().subscribe(
      (data: any[]) => {
        this.patients = data;
      },
      error => {
        console.error('Error fetching patients:', error);
        alert('Error fetching patient list!');
      }
    );
  }

  editRecord(record: any): void {
    this.medicalRecord = { ...record };
    this.isEditMode = true;
  }

  deleteRecord(id: number): void {
    if (confirm('Are you sure you want to delete this record?')) {
      this.hmsService.DeleteMedicalRecord(id).subscribe(
        () => {
          alert('Record deleted successfully!');
          this.getAllMedicalRecords();
        },
        error => {
          console.error('Error deleting record:', error);
          alert('Failed to delete record.');
        }
      );
    }
  }
  

  onSubmit(): void {
    if (this.isEditMode) {
      this.updateMedicalRecord();
    } else {
      this.createMedicalRecord();
    }
  }

  resetForm(): void {
    this.medicalRecord = {
      id: 0,
      patientId: 0,
      diagnosis: '',
      treatment: '',
      currentMedications: '',
      consultationNotes: '',
      followUpNotes: '',
      laboratoryResults: '',
      therapies: '',
      nextVisitDate: '',
      paymentRecords: '',
      imageUrls: ''
    };
    this.selectedFiles = [];
    this.isEditMode = false;
  }
}

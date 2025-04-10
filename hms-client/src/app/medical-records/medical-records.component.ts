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
    next_Visit_Date: '',
    paymentRecords: '',
    imageUrls: ''
  };
  
  selectedFile: File | null = null;
  patients: any[] = [];
  selectedFiles: File[] = [];
  medicalRecords: any[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPatients();
    this.getAllMedicalRecords();
  }

  getAllMedicalRecords(): void {
    this.hmsService.GetAllMedicalRecords().subscribe(
      (data: any) => {
        this.medicalRecords = data.map((record: any) => ({
          id: record.id,
          patientId: record.patientId,
          patientName: record.patientName,
          diagnosis: record.diagnosis,
          treatment: record.treatment,
          nextVisitDate: this.formatDate(record.next_Visit_Date),
          consultationNotes: record.consultation_Notes,
          followUpNotes: record.followUp_Notes,
          currentMedications: record.current_medications,
          laboratoryResults: record.laboratory_Results,
          therapies: record.therapies,
          paymentRecords: record.paymentRecords,
          imageUrls: record.imageUrls ? `https://localhost:7212${record.imageUrls}` : null
        }));
        console.log('Medical Records:', this.medicalRecords);
      },
      error => {
        console.error('Error fetching medical records:', error);
        alert('Error fetching medical records!');
      }
    );
  }
  

  private formatDate(dateString: string): string {
    if (!dateString) return '';
    try {
      const date = new Date(dateString);
      if (isNaN(date.getTime())) {
        console.warn('Invalid date string:', dateString);
        return dateString;
      }
      return date.toISOString().split('T')[0];
    } catch (e) {
      console.error('Date formatting error:', e);
      return dateString;
    }
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

  createMedicalRecord(): void {
    if (!this.medicalRecord.patientId || this.medicalRecord.patientId === 0) {
      alert("Please select a valid patient.");
      return;
    }

    const formData = new FormData();
    formData.append('patientId', this.medicalRecord.patientId.toString());
    formData.append('diagnosis', this.medicalRecord.diagnosis);
    formData.append('treatment', this.medicalRecord.treatment);
    formData.append('currentMedications', this.medicalRecord.currentMedications);
    formData.append('consultationNotes', this.medicalRecord.consultationNotes);
    formData.append('followUpNotes', this.medicalRecord.followUpNotes);
    formData.append('laboratoryResults', this.medicalRecord.laboratoryResults);
    formData.append('therapies', this.medicalRecord.therapies);
    formData.append('nextVisitDate', this.medicalRecord.next_Visit_Date);
    formData.append('paymentRecords', this.medicalRecord.paymentRecords);

    this.selectedFiles.forEach(file => {
      formData.append('images', file);
    });

    console.log("Sending FormData:", formData);

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
  selectedImageUrl: string | null = null;
  openImageModal(url: string) {
    this.selectedImageUrl = url;
  }

  closeImageModal() {
    this.selectedImageUrl = null;
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
    formData.append('nextVisitDate', this.medicalRecord.next_Visit_Date);
    formData.append('paymentRecords', this.medicalRecord.paymentRecords);

    this.selectedFiles.forEach(file => {
      formData.append('images', file);
    });

    console.log("Updating record with FormData:", formData);

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

  getPatients(): void {
    this.hmsService.GetPatient().subscribe(
      (data: any[]) => {
        this.patients = data;
        console.log("Patients loaded:", this.patients);
      },
      error => {
        console.error('Error fetching patients:', error);
        alert('Error fetching patient list!');
      }
    );
  }

  editRecord(record: any): void {
    console.log("Editing record:", record);
    this.medicalRecord = {
      ...record,
      next_Visit_Date: record.nextVisitDate
    };
    this.isEditMode = true;
  }
  
  onFileSelected(event: any): void {
    this.selectedFiles = Array.from(event.target.files);
    this.medicalRecord.imageUrls = '';
    console.log("Selected files:", this.selectedFiles);
  }
  
  deleteRecord(id: number): void {
    if (!id) {
      console.error('Invalid ID for deletion');
      alert('Invalid ID for deletion');
      return;
    }
    if (confirm('Are you sure you want to delete this record?')) {
      console.log('Deleting record with ID:', id);
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

  getPatientName(patientId: number): string {
    const patient = this.patients.find(p => p.id === patientId);
    return patient ? patient.name : 'Unknown';
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
      next_Visit_Date: '',
      paymentRecords: '',
      imageUrls: ''
    };
    this.selectedFiles = [];
    this.isEditMode = false;
  }
}

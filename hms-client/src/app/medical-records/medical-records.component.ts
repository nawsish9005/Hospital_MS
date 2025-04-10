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

  patients: any[] = [];
  selectedFiles: File[] = [];
  medicalRecords: any[] = [];
  isEditMode: boolean = false;

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPatients();
    this.getAllMedicalRecords();
  }

  onFileSelected(event: any): void {
    this.selectedFiles = Array.from(event.target.files);
    console.log("Selected files:", this.selectedFiles);
  }

  getAllMedicalRecords(): void {
    this.hmsService.GetAllMedicalRecords().subscribe(
      (data: any) => {
        this.medicalRecords = data.map((record: any) => ({
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
          imageUrls: record.imageUrls 
          ? `https://localhost:7212${record.imageUrls}` 
          : null
        }));
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

  deleteMedicalRecord(id: number): void {
    if (confirm('Are you sure you want to delete this medical record?')) {
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
    this.medicalRecord = {
      ...record,
      next_Visit_Date: record.nextVisitDate // Ensure this is editable again
    };
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

import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-medical-record',
  templateUrl: './medical-record.component.html',
  styleUrls: ['./medical-record.component.css']
})
export class MedicalRecordComponent implements OnInit{
  medicalRecords: any[] = [];

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getMedicalRecords();
  }

  getMedicalRecords(): void {
    this.hmsService.GetAllMedicalRecords().subscribe(data => {
      this.medicalRecords = data as any[];
    });
  }

  getMedicalRecordById(id: number): void {
    this.hmsService.GetMedicalRecordById(id).subscribe(data => {
      console.log(data);
    });
  }

  createMedicalRecord(medicalRecord: any): void {
    this.hmsService.CreateMedicalRecord(medicalRecord).subscribe(() => {
      this.getMedicalRecords();
    });
  }

  updateMedicalRecord(medicalRecord: any): void {
    this.hmsService.UpdateMedicalRecord(medicalRecord).subscribe(() => {
      this.getMedicalRecords();
    });
  }

  deleteMedicalRecord(id: number): void {
    this.hmsService.DeleteMedicalRecord(id).subscribe(() => {
      this.getMedicalRecords();
    });
  }
}

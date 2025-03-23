import { Component, OnInit } from '@angular/core';
import { HmsService } from '../services/hms.service';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css']
})
export class PrescriptionComponent implements OnInit{
  prescriptions: any[] = [];

  constructor(private hmsService: HmsService) {}

  ngOnInit(): void {
    this.getPrescriptions();
  }

  getPrescriptions(): void {
    this.hmsService.GetAllPrescriptions().subscribe(data => {
      this.prescriptions = data as any[];
    });
  }

  getPrescriptionById(id: number): void {
    this.hmsService.GetPrescriptionById(id).subscribe(data => {
      console.log(data);
    });
  }

  createPrescription(prescription: any): void {
    this.hmsService.CreatePrescription(prescription).subscribe(() => {
      this.getPrescriptions();
    });
  }

  updatePrescription(prescription: any): void {
    this.hmsService.UpdatePrescription(prescription).subscribe(() => {
      this.getPrescriptions();
    });
  }

  deletePrescription(id: number): void {
    this.hmsService.DeletePrescription(id).subscribe(() => {
      this.getPrescriptions();
    });
  }
}

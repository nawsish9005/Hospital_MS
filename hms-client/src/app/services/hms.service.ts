import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HmsService {
  private baseUrl = 'https://localhost:7212/api';
  
  constructor(private http: HttpClient) {}
  
  //https://localhost:7212/api/Appointment

  public appointmentUrl = "/Appointment";

  public GetAllAppointments(){
    return this.http.get(this.baseUrl + this.appointmentUrl);
   }

   public GetAppointmentById(id: number){
    return this.http.get(this.baseUrl + this.appointmentUrl + "/" + id);
  }

  public CreateAppointment(data: any){
    return this.http.post(this.baseUrl + this.appointmentUrl, data);
  }
  
  public UpdateAppointment(data: any){
    return this.http.put(this.baseUrl + this.appointmentUrl, data)
  }
  
  public DeleteAppointment(id: number){
    return this.http.delete(this.baseUrl + this.appointmentUrl + "/" + id);
  }


  //https://localhost:7212/api/Doctor
  
  public doctorUrl = "/Doctor";

  public GetDoctor(){
    return this.http.get(this.baseUrl + this.doctorUrl);
   }

   public GetDoctorById(id: number){
    return this.http.get(this.baseUrl + this.doctorUrl + "/" + id);
  }

  public CreateDoctor(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}${this.doctorUrl}`, data);
  }
  
  public UpdateDoctor(id: number, data: FormData): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}${this.doctorUrl}/${id}`, data);
}
  
  public DeleteDoctor(id: number){
    return this.http.delete(this.baseUrl + this.doctorUrl + "/" + id);
  }

  //https://localhost:7212/api/Prescription
  
  public prescriptionUrl = "/Prescription";

  public GetAllPrescriptions(){
    return this.http.get(this.baseUrl + this.prescriptionUrl);
   }

   public GetPrescriptionById(id: number){
    return this.http.get(this.baseUrl + this.prescriptionUrl + "/" + id);
  }

  public CreatePrescription(data: any){
    return this.http.post(this.baseUrl + this.prescriptionUrl, data);
  }
  
  public UpdatePrescription(data: any){
    return this.http.put(this.baseUrl + this.prescriptionUrl, data)
  }
  
  public DeletePrescription(id: number){
    return this.http.delete(this.baseUrl + this.prescriptionUrl + "/" + id);
  }

  //https://localhost:7212/api/MedicalRecord
  
  public medicalRecordUrl = "/MedicalRecord";

  public GetAllMedicalRecords(){
    return this.http.get(this.baseUrl + this.medicalRecordUrl);
   }

   public GetMedicalRecordById(id: number){
    return this.http.get(this.baseUrl + this.medicalRecordUrl + "/" + id);
  }

  public CreateMedicalRecord(data: any){
    return this.http.post(this.baseUrl + this.medicalRecordUrl, data);
  }
  
  public UpdateMedicalRecord(data: any, formData: FormData){
    return this.http.put(this.baseUrl + this.medicalRecordUrl, data)
  }
  
  public DeleteMedicalRecord(id: number){
    return this.http.delete(this.baseUrl + this.medicalRecordUrl + "/" + id);
  }

  //https://localhost:7212/api/Patient
  
  public patientUrl = "/Patient";

  public GetPatient(){
    return this.http.get(this.baseUrl + this.patientUrl);
   }

   public GetPatientById(id: number){
    return this.http.get(this.baseUrl + this.patientUrl + "/" + id);
  }

  public CreatePatient(data: any){
    return this.http.post(this.baseUrl + this.patientUrl, data);
  }
  
  public UpdatePatient(data: any, formData: FormData){
    return this.http.put(this.baseUrl + this.patientUrl, data)
  }
  
  public DeletePatient(id: number){
    return this.http.delete(this.baseUrl + this.patientUrl + "/" + id);
  }

}

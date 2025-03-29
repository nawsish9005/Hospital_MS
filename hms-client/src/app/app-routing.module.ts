import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './frontEnd/home/home.component';
import { DoctorComponent } from './doctor/doctor.component';
import { PatientComponent } from './patient/patient.component';
import { MedicalRecordsComponent } from './medical-records/medical-records.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'doctor',title:'Doctor' ,component: DoctorComponent},
  {path: 'patient',title:'Patient' ,component: PatientComponent},
  {path: 'medicalRecords',title:'Medical Record' ,component: MedicalRecordsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

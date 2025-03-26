import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorListComponent } from './frontEnd/doctor-list/doctor-list.component';
import { DoctorComponent } from './doctor/doctor.component';
import { NavbarComponent } from './frontEnd/navbar/navbar.component';
import { PatientComponent } from './patient/patient.component';

const routes: Routes = [
  { path: '', component: NavbarComponent},
  { path: 'doctor', title:'Doctor Submit', component: DoctorComponent},
  { path: 'doctor-list',title:'Doctor List', component: DoctorListComponent },
  { path: 'patient',title:'Patient', component: PatientComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

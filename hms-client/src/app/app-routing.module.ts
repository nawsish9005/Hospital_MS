import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorListComponent } from './frontEnd/doctor-list/doctor-list.component';
import { DoctorComponent } from './doctor/doctor.component';
import { NavbarComponent } from './frontEnd/navbar/navbar.component';

const routes: Routes = [
  { path: '', title:'Doctor Submit', component: NavbarComponent},
  { path: 'doctor', title:'Doctor Submit', component: DoctorComponent},
  { path: 'doctor-list',title:'Doctor List', component: DoctorListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

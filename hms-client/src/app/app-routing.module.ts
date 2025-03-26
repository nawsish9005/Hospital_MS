import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorListComponent } from './frontEnd/doctor-list/doctor-list.component';
import { DoctorComponent } from './doctor/doctor.component';
import { NavbarComponent } from './frontEnd/navbar/navbar.component';
import { FooterComponent } from './frontEnd/footer/footer.component';

const routes: Routes = [
  { path: '', title:'DoctorHome', component: NavbarComponent},
  { path: 'doctor', title:'Doctor Submit', component: DoctorComponent},
  { path: 'doctor-list',title:'Doctor List', component: DoctorListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

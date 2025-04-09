import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './frontEnd/home/home.component';
import { DoctorListComponent } from './frontEnd/doctor-list/doctor-list.component';
import { PatientListComponent } from './frontEnd/patient-list/patient-list.component';
import { PatientComponent } from './patient/patient.component';
import { DoctorComponent } from './doctor/doctor.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PreloaderComponent } from './frontEnd/preloader/preloader.component';
import { MedicalRecordsComponent } from './medical-records/medical-records.component';
import { PrescriptionComponent } from './prescription/prescription.component';
import { FooterComponent } from './frontEnd/footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DoctorListComponent,
    PatientListComponent,
    PatientComponent,
    DoctorComponent,
    PreloaderComponent,
    MedicalRecordsComponent,
    PrescriptionComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

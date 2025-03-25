import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { DoctorComponent } from './doctor/doctor.component';
import { PatientComponent } from './patient/patient.component';
import { PrescriptionComponent } from './prescription/prescription.component';
import { MedicalRecordComponent } from './medical-record/medical-record.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DoctorListComponent } from './frontEnd/doctor-list/doctor-list.component';
import { NavbarComponent } from './frontEnd/navbar/navbar.component';
import { PreloaderComponent } from './frontEnd/preloader/preloader.component';
import { FooterComponent } from './frontEnd/footer/footer.component';
import { HeaderComponent } from './frontEnd/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    AppointmentComponent,
    DoctorComponent,
    PatientComponent,
    PrescriptionComponent,
    MedicalRecordComponent,
    DoctorListComponent,
    NavbarComponent,
    PreloaderComponent,
    FooterComponent,
    HeaderComponent,
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

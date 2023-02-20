import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { AlertComponent } from './components/alert/alert.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';
import { PatientComponent } from './components/patient/patient.component';
import { DriverComponent } from './components/driver/driver.component';
import { ServerDashboardComponent } from './components/server-dashboard/server-dashboard.component';
import { PatientRouteComponent } from './components/patient-route/patient-route.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FetchDataComponent,
    LoginComponent,
    AlertComponent,
    VehiclesComponent,
    PatientComponent,
    DriverComponent,
    ServerDashboardComponent,
    PatientRouteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'dashboard', component: ServerDashboardComponent },
      { path: 'requests', component: PatientRouteComponent },
      { path: 'vehicles', component: VehiclesComponent },
      { path: 'patients', component: PatientComponent },
      { path: 'drivers', component: DriverComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent }
    ])
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

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
import {ServerGuard} from "./infrastructure/server.guard";
import { DriverHomeComponent } from './components/driver-home/driver-home.component';
import {DriverGuard} from "./infrastructure/driver.guard";
import {JwtInterceptor} from "./infrastructure/jwt.interceptor";
import {AuthenticationInterceptor} from "./infrastructure/authentication.interceptor";

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
    PatientRouteComponent,
    DriverHomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'dashboard', component: ServerDashboardComponent, canActivate: [ServerGuard]},
      { path: 'requests', component: PatientRouteComponent, canActivate: [ServerGuard] },
      { path: 'vehicles', component: VehiclesComponent, canActivate: [ServerGuard] },
      { path: 'patients', component: PatientComponent, canActivate: [ServerGuard] },
      { path: 'drivers', component: DriverComponent, canActivate: [ServerGuard] },
      { path: 'driver-home', component: DriverHomeComponent, canActivate: [DriverGuard] },
      { path: 'login', component: LoginComponent }
    ])
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

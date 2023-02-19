import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Driver} from "../models/driver";

@Injectable({
  providedIn: 'root'
})
export class DriverService {

  constructor(private httpClient: HttpClient) { }

  getDrivers(): Observable<Driver[]> {
    const uri = 'api/v1/Driver';
    return this.httpClient.get<Driver[]>(uri);
  }

  addDriver(driver: Driver): Observable<Driver> {
    const uri = 'api/v1/Driver';
    return this.httpClient.post<Driver>(uri, driver);
  }
}

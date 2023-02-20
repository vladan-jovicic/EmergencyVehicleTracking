import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {DriverState} from "../models/driver-state";
import {Vehicle} from "../models/vehicle";

@Injectable({
  providedIn: 'root'
})
export class DriverStateService {

  constructor(private httpClient: HttpClient) { }

  getState(): Observable<DriverState> {
    const uri = 'api/v1/DriverState';
    return this.httpClient.get<DriverState>(uri);
  }

  getVehicles(): Observable<Vehicle[]> {
    const uri = 'api/v1/DriverState/Vehicles';
    return this.httpClient.get<Vehicle[]>(uri);
  }
}

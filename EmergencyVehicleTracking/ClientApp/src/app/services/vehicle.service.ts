import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Vehicle} from "../models/vehicle";

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private httpClient: HttpClient) { }

  getVehicles(): Observable<Vehicle[]> {
    const uri = 'api/v1/Vehicle';
    return this.httpClient.get<Vehicle[]>(uri);
  }

  addVehicle(vehicle: Vehicle): Observable<Vehicle> {
    const uri = 'api/v1/Vehicle';
    return this.httpClient.post<Vehicle>(uri, vehicle);
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {PatientPickUpRequest} from "../models/patient-pick-up-request";

@Injectable({
  providedIn: 'root'
})
export class PatientPickUpService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<PatientPickUpRequest[]> {
    const uri = 'api/v1/PatientRequest';
    return this.httpClient.get<PatientPickUpRequest[]>(uri);
  }

  add(request: PatientPickUpRequest): Observable<PatientPickUpRequest> {
    const uri = 'api/v1/PatientRequest';
    return this.httpClient.post<PatientPickUpRequest>(uri, request);
  }
}

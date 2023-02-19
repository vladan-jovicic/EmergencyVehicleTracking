import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Patient} from "../models/patient";

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Patient[]> {
    const uri = 'api/v1/Patient';
    return this.httpClient.get<Patient[]>(uri);
  }

  add(patient: Patient): Observable<Patient> {
    const uri = 'api/v1/Patient';
    return this.httpClient.post<Patient>(uri, patient);
  }
}

import { Component, OnInit } from '@angular/core';
import {PatientPickUpRequest} from "../../models/patient-pick-up-request";
import {PatientPickUpService} from "../../services/patient-pick-up.service";
import {Patient} from "../../models/patient";
import {PatientService} from "../../services/patient.service";

@Component({
  selector: 'app-patient-route',
  templateUrl: './patient-route.component.html',
  styleUrls: ['./patient-route.component.css']
})
export class PatientRouteComponent implements OnInit {

  patientRequests: PatientPickUpRequest[] = [];
  patients: Patient[] = [];
  newRequest: PatientPickUpRequest = {
    id: 0,
    patientId: 0,
    pickUpLocation: {x: 0, y: 0},
    dropOffLocation: {x: 0, y: 0},
    status: 'Waiting'
  };

  constructor(private patientPickUpService: PatientPickUpService,
              private patientService: PatientService) { }

  ngOnInit(): void {
    // initialize requests
    this.patientPickUpService.getAll().subscribe(
      res => {
        this.patientRequests = res;
      }
    );

    this.patientService.getAll().subscribe(res => {
      this.patients = res;
    });
  }


  selectPatient(value: string): void {
    this.newRequest.patientId = Number.parseInt(value);
  }

  addNew(): void {
    console.log(this.newRequest);
    this.patientPickUpService.add(this.newRequest).subscribe(
      res => {
        this.patientRequests.push(res);
        this.newRequest = {
          id: 0,
          patientId: 0,
          pickUpLocation: {x: 0, y: 0},
          dropOffLocation: {x: 0, y: 0},
          status: 'Waiting'
        };
      }
    )
  }

  getPatientName(id: number): string {
    const patient = this.patients.find(i => i.id == id);
    return patient === undefined ? 'N/A' : `${patient.firstName} ${patient.lastName}`;
  }

}

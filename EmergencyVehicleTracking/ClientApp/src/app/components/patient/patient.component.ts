import { Component, OnInit } from '@angular/core';
import {Patient} from "../../models/patient";
import {PatientService} from "../../services/patient.service";

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.scss']
})
export class PatientComponent implements OnInit {

  patients: Patient[] = [];
  newPatient: Patient = {
    id: 0,
    firstName: '',
    address: '',
    city: '',
    country: '',
    lastName: ''
  };

  constructor(private patientService: PatientService) { }

  ngOnInit(): void {
    this.patientService.getAll().subscribe(
      res => {
        this.patients = res;
      }
    )
  }

  addNew(): void {
    this.patientService.add(this.newPatient).subscribe(
      res => {
        this.patients.push(res);
        this.newPatient = {
          id: 0,
          firstName: '',
          address: '',
          city: '',
          country: '',
          lastName: ''
        };
      }
    )
  }

}

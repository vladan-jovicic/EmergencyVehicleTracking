import { Component, OnInit } from '@angular/core';
import {VehicleService} from "../../services/vehicle.service";
import {Vehicle} from "../../models/vehicle";
import {AlertService} from "../../services/alert.service";

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {

  vehicles: Vehicle[] = [];
  newVehicle: Vehicle = {
    id: 0,
    name: '',
    type: 'Van',
    registrationNumber: ''
  };
  addNewDialog: boolean = false;

  constructor(private vehicleService: VehicleService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.vehicleService.getVehicles().subscribe(
      res => {
        this.vehicles = res;
      },
      err => {
        this.alertService.error('Failed to get vehicle list.');
      }
    )
  }

  addNewVehicle(): void {
    this.vehicleService.addVehicle(this.newVehicle).subscribe(
      res => {
        this.vehicles.push(res);
        this.newVehicle = {
          id: 0,
          name: '',
          type: 'Van',
          registrationNumber: ''
        };
      },
      err => {
        this.alertService.error('Failed to add new vehicle');
      }
    )
  }


}

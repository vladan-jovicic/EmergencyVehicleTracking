import { Component, OnInit } from '@angular/core';
import {DriverState} from "../../models/driver-state";
import {DriverStateService} from "../../services/driver-state.service";
import {Vehicle} from "../../models/vehicle";

@Component({
  selector: 'app-driver-home',
  templateUrl: './driver-home.component.html',
  styleUrls: ['./driver-home.component.css']
})
export class DriverHomeComponent implements OnInit {

  driverState: DriverState | undefined;
  vehicles: Vehicle[] = [];

  constructor(private driverStateService: DriverStateService) { }

  ngOnInit(): void {
    this.driverStateService.getState().subscribe(res => {
      this.driverState = res;

      if (this.driverState.state == "SelectVehicle") {
        this.driverStateService.getVehicles().subscribe(vehicles => {
          this.vehicles = vehicles;
        })
      }

    });
  }

}

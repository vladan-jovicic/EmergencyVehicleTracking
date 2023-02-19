import { Component, OnInit } from '@angular/core';
import {Driver} from "../../models/driver";
import {DriverService} from "../../services/driver.service";

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.css']
})
export class DriverComponent implements OnInit {

  drivers: Driver[] = [];
  newDriver: Driver = {
    id: 0,
    lastName: '',
    firstName: '',
    location: { x: 0, y: 0},
    perimeter: 100
  };


  constructor(private driverService: DriverService) { }

  ngOnInit(): void {
    this.driverService.getDrivers().subscribe(
      res => {
        this.drivers = res;
      }
    );
  }

  addNewDriver(): void {
    this.driverService.addDriver(this.newDriver).subscribe(
      res => {
        this.drivers.push(res);
        this.newDriver = {
          id: 0,
          lastName: '',
          firstName: '',
          location: { x: 0, y: 0},
          perimeter: 100
        };
      }
    );
  }

}

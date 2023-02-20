import {Driver} from "./driver";

export interface DriverState {
  driverId: number;
  driver: Driver;
  selectedVehicle?: number;
  routeId?: number;
  state: string;
}

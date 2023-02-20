import {Coordinates} from "./coordinates";

export interface PatientPickUpRequest {
  id: number;
  patientId: number;
  pickUpLocation: Coordinates;
  dropOffLocation: Coordinates;
  status: string;
}

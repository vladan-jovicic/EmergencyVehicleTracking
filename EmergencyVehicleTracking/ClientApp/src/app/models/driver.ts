import {Coordinates} from "./coordinates";

export interface Driver {
  id: number;
  firstName: string;
  lastName: string;
  location: Coordinates;
  perimeter: number;
}

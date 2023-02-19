import { TestBed } from '@angular/core/testing';

import { PatientPickUpService } from './patient-pick-up.service';

describe('PatientPickUpService', () => {
  let service: PatientPickUpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientPickUpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

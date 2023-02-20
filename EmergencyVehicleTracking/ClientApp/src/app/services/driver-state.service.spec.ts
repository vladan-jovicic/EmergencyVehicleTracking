import { TestBed } from '@angular/core/testing';

import { DriverStateService } from './driver-state.service';

describe('DriverStateService', () => {
  let service: DriverStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DriverStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

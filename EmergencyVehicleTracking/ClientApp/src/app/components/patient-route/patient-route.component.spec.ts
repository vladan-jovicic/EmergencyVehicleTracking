import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientRouteComponent } from './patient-route.component';

describe('PatientRouteComponent', () => {
  let component: PatientRouteComponent;
  let fixture: ComponentFixture<PatientRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

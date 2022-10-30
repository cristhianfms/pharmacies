import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmacyListComponent } from './pharmacy-list.component';

describe('PharmacyListComponent', () => {
  let component: PharmacyListComponent;
  let fixture: ComponentFixture<PharmacyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PharmacyListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PharmacyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

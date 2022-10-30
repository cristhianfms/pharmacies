import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmacyCreateComponent } from './pharmacy-create.component';

describe('PharmacyCreateComponent', () => {
  let component: PharmacyCreateComponent;
  let fixture: ComponentFixture<PharmacyCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PharmacyCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PharmacyCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

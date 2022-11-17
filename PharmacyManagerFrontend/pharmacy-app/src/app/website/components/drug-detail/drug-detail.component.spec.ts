import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrugDetailComponent } from './drug-detail.component';

describe('DrugDetailComponent', () => {
  let component: DrugDetailComponent;
  let fixture: ComponentFixture<DrugDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DrugDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrugDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

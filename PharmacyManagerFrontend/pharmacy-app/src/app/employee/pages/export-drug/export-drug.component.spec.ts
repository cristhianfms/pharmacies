import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportDrugComponent } from './export-drug.component';

describe('ExportDrugComponent', () => {
  let component: ExportDrugComponent;
  let fixture: ComponentFixture<ExportDrugComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExportDrugComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExportDrugComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

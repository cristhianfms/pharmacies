import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrugListDeleteComponent } from './drug-list-delete.component';

describe('DrugListDeleteComponent', () => {
  let component: DrugListDeleteComponent;
  let fixture: ComponentFixture<DrugListDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DrugListDeleteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrugListDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

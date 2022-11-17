import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitudesUpdateComponent } from './solicitudes-update.component';

describe('SolicitudesComponent', () => {
  let component: SolicitudesUpdateComponent;
  let fixture: ComponentFixture<SolicitudesUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitudesUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolicitudesUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

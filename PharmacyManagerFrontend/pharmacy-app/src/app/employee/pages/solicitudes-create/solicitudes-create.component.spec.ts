import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitudesCreateComponent } from './solicitudes-create.component';

describe('SolicitudesCreateComponent', () => {
  let component: SolicitudesCreateComponent;
  let fixture: ComponentFixture<SolicitudesCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitudesCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolicitudesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitudesListComponent } from './solicitudes-list.component';

describe('SolicitudesListComponent', () => {
  let component: SolicitudesListComponent;
  let fixture: ComponentFixture<SolicitudesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitudesListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolicitudesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationCreateComponent } from './invitation-create.component';

describe('InvitationCreateComponent', () => {
  let component: InvitationCreateComponent;
  let fixture: ComponentFixture<InvitationCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvitationCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitationCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

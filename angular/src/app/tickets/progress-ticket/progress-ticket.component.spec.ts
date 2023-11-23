import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressTicketComponent } from './progress-ticket.component';

describe('ProgressTicketComponent', () => {
  let component: ProgressTicketComponent;
  let fixture: ComponentFixture<ProgressTicketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgressTicketComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProgressTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

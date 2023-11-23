import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateColumnStatusComponent } from './create-column-status.component';

describe('CreateColumnStatusComponent', () => {
  let component: CreateColumnStatusComponent;
  let fixture: ComponentFixture<CreateColumnStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateColumnStatusComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateColumnStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

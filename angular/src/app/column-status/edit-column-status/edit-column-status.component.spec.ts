import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditColumnStatusComponent } from './edit-column-status.component';

describe('EditColumnStatusComponent', () => {
  let component: EditColumnStatusComponent;
  let fixture: ComponentFixture<EditColumnStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditColumnStatusComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditColumnStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

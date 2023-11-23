import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColumnStatusesComponent } from './column-statuses.component';

describe('ColumnStatusesComponent', () => {
  let component: ColumnStatusesComponent;
  let fixture: ComponentFixture<ColumnStatusesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColumnStatusesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColumnStatusesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

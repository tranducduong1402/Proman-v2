import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressTaskComponent } from './progress-task.component';

describe('ProgressTaskComponent', () => {
  let component: ProgressTaskComponent;
  let fixture: ComponentFixture<ProgressTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgressTaskComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProgressTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

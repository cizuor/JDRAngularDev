import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewStatUtilComponent } from './new-stat-util.component';

describe('NewStatUtilComponent', () => {
  let component: NewStatUtilComponent;
  let fixture: ComponentFixture<NewStatUtilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewStatUtilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewStatUtilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

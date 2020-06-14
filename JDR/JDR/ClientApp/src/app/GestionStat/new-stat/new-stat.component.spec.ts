import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewStatComponent } from './new-stat.component';

describe('NewStatComponent', () => {
  let component: NewStatComponent;
  let fixture: ComponentFixture<NewStatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewStatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewStatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllsousraceComponent } from './allsousrace.component';

describe('AllsousraceComponent', () => {
  let component: AllsousraceComponent;
  let fixture: ComponentFixture<AllsousraceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllsousraceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllsousraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

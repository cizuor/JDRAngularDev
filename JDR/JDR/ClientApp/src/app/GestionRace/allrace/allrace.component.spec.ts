import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllraceComponent } from './allrace.component';

describe('AllraceComponent', () => {
  let component: AllraceComponent;
  let fixture: ComponentFixture<AllraceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllraceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

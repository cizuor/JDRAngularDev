import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllstatComponent } from './allstat.component';

describe('AllstatComponent', () => {
  let component: AllstatComponent;
  let fixture: ComponentFixture<AllstatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllstatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllstatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
